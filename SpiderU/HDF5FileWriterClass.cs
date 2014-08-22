﻿using System;
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

				string GrooupComment = SList[0].Comment;
				for (int SIndex = 1; SIndex < SList.Count; SIndex++) {
					GrooupComment = GrooupComment + SList[SIndex].Comment;
				}
				if (GrooupComment != null) {
					char[] GroupCommentChar = AEncoding.GetChars(AEncoding.GetBytes(GrooupComment));
					long[] commentDimension = new long[1];
					commentDimension[0] = GroupCommentChar.Length;
					H5DataSpaceId commentSpaceID = H5S.create_simple(1, commentDimension);
					H5AttributeId commentAttrID = H5A.create(scopeGroupID, "comment", UCharTypeID, commentSpaceID);
					H5A.write<char>(commentAttrID, UCharTypeID, new H5Array<char>((GroupCommentChar)));
					H5A.close(commentAttrID);
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

				byte[] labelByte = AEncoding.GetBytes("stime"); 
				long[] labelDimension = new long[1];
				labelDimension[0] = labelByte.Length;
				H5DataSpaceId labelSpaceID = H5S.create_simple(1, labelDimension);
				H5AttributeId labelID = H5A.create(traceDataSetID, "label", UCharTypeID, labelSpaceID);
				H5A.write<byte>(labelID, UCharTypeID, new H5Array<byte>((labelByte)));
				H5A.close(labelID);

				byte[] unitByte = AEncoding.GetBytes("s");
				long[] unitDimension = new long[1];
				unitDimension[0] = unitByte.Length;
				H5DataSpaceId unitSpaceID = H5S.create_simple(1, unitDimension);
				H5AttributeId unitID = H5A.create(traceDataSetID, "unit", UCharTypeID, unitSpaceID);
				H5A.write<byte>(unitID, UCharTypeID, new H5Array<byte>((unitByte)));
				H5A.close(unitID);

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
						labelByte = AEncoding.GetBytes(SList[SIndex].OnTrace[TIndex].TraceLabel);
						labelDimension[0] = labelByte.Length;
						labelSpaceID = H5S.create_simple(1, labelDimension);
						labelID = H5A.create(traceDataSetID, "label", UCharTypeID, labelSpaceID);
						H5A.write<byte>(labelID, UCharTypeID, new H5Array<byte>((labelByte)));
						H5A.close(labelID);

						unitByte = AEncoding.GetBytes(SList[SIndex].OnTrace[TIndex].TraceUnit);
						unitDimension[0] = unitByte.Length;
						unitSpaceID = H5S.create_simple(1, unitDimension);
						unitID = H5A.create(traceDataSetID, "unit", UCharTypeID, unitSpaceID);
						H5A.write<byte>(unitID, UCharTypeID, new H5Array<byte>((unitByte)));
						H5A.close(unitID);

						H5D.write<double>(traceDataSetID, doubleTypeId, new H5Array<double>(SList[SIndex].OnTrace[TIndex].Data));
						H5D.close(traceDataSetID);
						traceIDNum++;
					}
				}
				H5G.close(scopeGroupID);
			} else {
				for (int SIndex = 1; SIndex < SList.Count; SIndex++) {
					ScopeClass Scope = SList[SIndex];
					string groupIDString = string.Format("Oscilloscope{0:d}", SIndex + 1);
					H5GroupId groupID = H5G.create(fileID, groupIDString);

					if (Scope.Comment != null) {
						char[] CommentChar = AEncoding.GetChars(AEncoding.GetBytes(Scope.Comment));
						long[] commentDimension = new long[1];
						commentDimension[0] = CommentChar.Length;
						H5DataSpaceId commentSpaceID = H5S.create_simple(1, commentDimension);
						H5AttributeId commentAttrID = H5A.create(groupID, "comment", UCharTypeID, commentSpaceID);
						H5A.write<char>(commentAttrID, UCharTypeID, new H5Array<char>((CommentChar)));
						H5A.close(commentAttrID);
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
