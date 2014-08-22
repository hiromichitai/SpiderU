using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HDF5DotNet;

namespace SpiderU {
	class HDF5FileWriterClass: FileWriterClass {
		private H5FileId fileID;
		private Encoding AEncoding = Encoding.GetEncoding("us-ascii",
			new EncoderReplacementFallback("?"), new DecoderReplacementFallback("?"));


		public HDF5FileWriterClass(string FileName) : base(FileName) {
			try {
				fileID = H5F.create(FileName, H5F.CreateMode.ACC_TRUNC);
			}
			catch (ArgumentException) {
				WarningDialog WDialog = new WarningDialog("UIMSGARGEXCEPTION", "in HDF5FileWriter");
			}
			catch (System.Security.SecurityException) {
				WarningDialog WDialog = new WarningDialog("UIMSGSECURITYEXCEPTION", "in HDF5FileWriter");
			}
		}

		private void WriteAttributeString(string attributeName, string aString, H5ObjectWithAttributes rootID) {
			H5DataTypeId UCharTypeID = H5T.copy(H5T.H5Type.NATIVE_UCHAR);
			byte[] stringByte = AEncoding.GetBytes(aString);
			long[] stringDimension = new long[1];
			stringDimension[0] = stringByte.Length;
			H5DataSpaceId stringSpaceID = H5S.create_simple(1, stringDimension);
			H5AttributeId attributeID = H5A.create(rootID, attributeName, UCharTypeID, stringSpaceID);
			H5A.write<byte>(attributeID, UCharTypeID, new H5Array<byte>((stringByte)));
			H5A.close(attributeID);
		}


		public override void WriteFile() {

			H5DataTypeId UCharTypeID = H5T.copy(H5T.H5Type.NATIVE_UCHAR);
			H5DataTypeId doubleTypeId = H5T.copy(H5T.H5Type.NATIVE_DOUBLE);
			H5T.setOrder(doubleTypeId, H5T.Order.LE);

			List<ScopeClass> SList = ScopeManager.ScopeList;
			if (Properties.Settings.Default.syncAllScope) {
				if (!ScopeManager.AllScopeSyncable()) {
					WarningDialog WDialog = new WarningDialog("UIMSGNOTSYNCABLE", true);
					return;
				}
				H5GroupId scopeGroupID = H5G.create(fileID, "Oscilloscope1");	// one group for one oscilloscope

				string GroupComment = SList[0].Comment;
				for (int SIndex = 1; SIndex < SList.Count; SIndex++) {
					GroupComment = GroupComment + SList[SIndex].Comment;
				}
				if (GroupComment != null) {
					WriteAttributeString("comment", GroupComment, scopeGroupID);
				}

				long[] dataDimension = new long[1];
				dataDimension[0] = SList[0].DataLength; 					// one dataset for one trace
				H5DataSpaceId traceDataSpaceId = H5S.create_simple(1, dataDimension);

				int NumTrace = 1;	// +1 for sampling time
				foreach (ScopeClass Scope in SList) {
					NumTrace += Scope.OnTrace.Count;
				}
				string traceIDNumFormat = "Trace{0:";
				for (int NTrace = NumTrace; NTrace > 1; NTrace /= 10) {
					traceIDNumFormat += "0";
				}
				traceIDNumFormat += "}";

				H5DataSetId traceDataSetID = H5D.create(scopeGroupID,
					"stime", doubleTypeId, traceDataSpaceId);
				WriteAttributeString("label", "stime", traceDataSetID);
				WriteAttributeString("unit", "s", traceDataSetID);

				double [] stime = new double[SList[0].DataLength];
				for(int DIndex=0; DIndex < stime.Length; DIndex++){
					stime[DIndex] = DIndex*SList[0].SampleTime;
				}
				H5D.write<double>(traceDataSetID, doubleTypeId, new H5Array<double>(stime));
				H5D.close(traceDataSetID);

				int traceIDNum = 1;
				for (int SIndex = 0; SIndex < SList.Count; SIndex++) {
					for (int TIndex = 0; TIndex < SList[SIndex].OnTrace.Count; TIndex++) {
						string traceIDString = String.Format(traceIDNumFormat, traceIDNum);
						traceDataSetID = H5D.create(scopeGroupID,
							traceIDString, doubleTypeId, traceDataSpaceId);
						WriteAttributeString("label", SList[SIndex].OnTrace[TIndex].TraceLabel, traceDataSetID);
						WriteAttributeString("unit", SList[SIndex].OnTrace[TIndex].TraceUnit, traceDataSetID);

						H5D.write<double>(traceDataSetID, doubleTypeId, new H5Array<double>(SList[SIndex].OnTrace[TIndex].Data));
						H5D.close(traceDataSetID);
						traceIDNum++;
					}
				}
				H5G.close(scopeGroupID);
			} else {
				for (int SIndex = 0; SIndex < SList.Count; SIndex++) {
					ScopeClass Scope = SList[SIndex];
					string groupIDString = string.Format("DSO{0:d}", SIndex + 1);
					H5GroupId groupID = H5G.create(fileID, groupIDString);

					if (Scope.Comment != null) {
						WriteAttributeString("comment", Scope.Comment, groupID);
					}

					long[] dataDimension = new long[1];
					dataDimension[0] = Scope.DataLength; 					// one dataset for one trace
					H5DataSpaceId traceDataSpaceId = H5S.create_simple(1, dataDimension);

					H5DataSetId traceDataSetID = H5D.create(groupID,"stime", doubleTypeId, traceDataSpaceId);

					WriteAttributeString("label", "stime", traceDataSetID);
					WriteAttributeString("unit", "s", traceDataSetID);

					double[] stime = new double[SList[0].DataLength];
					for (int DIndex = 0; DIndex < stime.Length; DIndex++) {
						stime[DIndex] = DIndex * SList[0].SampleTime;
					}
					H5D.write<double>(traceDataSetID, doubleTypeId, new H5Array<double>(stime));
					H5D.close(traceDataSetID);

					for (int TIndex = 0; TIndex < Scope.OnTrace.Count; TIndex++) {
						H5DataSetId dataSetID = H5D.create(fileID, "/" + Scope.ChannelLabel(TIndex), doubleTypeId, traceDataSpaceId);
						WriteAttributeString("label", Scope.OnTrace[TIndex].TraceLabel, dataSetID);
						WriteAttributeString("unit", Scope.OnTrace[TIndex].TraceUnit, dataSetID);

						H5D.write<double>(dataSetID, doubleTypeId, new H5Array<double>(Scope.OnTrace[TIndex].Data));
						H5D.close(dataSetID);
					}
					H5G.close(groupID);
				}

			}

		}

		public override void Close() {
			H5F.close(fileID);
		}

	}
}
