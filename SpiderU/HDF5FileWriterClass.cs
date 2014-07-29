using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HDF5DotNet;

namespace SpiderU {
	class HDF5FileWriterClass: FileWriterClass {
		private H5FileId fileID;

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


		public override void WriteFile() {
			H5DataTypeId UCharTypeID = H5T.copy(H5T.H5Type.NATIVE_UCHAR);

			List<ScopeClass> SList = ScopeManager.ScopeList;
			if (Properties.Settings.Default.syncAllScope) {
				if (!ScopeManager.AllScopeSyncable()) {
					WarningDialog WDialog = new WarningDialog("UIMSGNOTSYNCABLE", true);
					return;
				}
				H5GroupId groupID = H5G.create(fileID, "Oscilloscope1");
				int NumTrace = 1;
				foreach (ScopeClass Scope in SList) {
					NumTrace += Scope.OnTrace.Count;
				}
				long[] dataDimension = new long[2];
				dataDimension[0] = NumTrace;
				dataDimension[1] = SList[0].DataLength;
				H5DataSpaceId dataSpaceId = H5S.create_simple(1, dataDimension);
				H5DataTypeId doubleTypeId = H5T.copy(H5T.H5Type.NATIVE_DOUBLE);
				H5DataSetId dataSetID = H5D.create(fileID, "/" + "Oscilloscope1", doubleTypeId, dataSpaceId);

				if (Properties.Settings.Default.addComment) {
					string GrooupComment = SList[0].Comment;
					for (int SIndex = 1; SIndex < SList.Count; SIndex++) {
						GrooupComment = GrooupComment + SList[SIndex].Comment;
					}
					long[] commentDimension = new long[1];
					commentDimension[0] = GrooupComment.Length;
					H5DataSpaceId commentSpaceID = H5S.create_simple(1, commentDimension);
					H5AttributeId comment = H5A.create(dataSetID, "comment", UCharTypeID, commentSpaceID);
					H5A.write<char>(comment, UCharTypeID, new H5Array<char>(Scope.OnTrace[TIndex].TraceLabel.ToCharArray()));
					for (int SIndex = 1; SIndex < SList.Count; SIndex++) {
						ScopeClass Scope = SList[SIndex];
						for (int TIndex = 0; TIndex < Scope.OnTrace.Count; TIndex++) {
							H5AttributeId label = H5A.create(dataSetID, "label", UCharTypeID, labelSpaceID);
							H5A.write<char>(label, UCharTypeID, new H5Array<char>(Scope.OnTrace[TIndex].TraceLabel.ToCharArray()));
							H5AttributeId unit = H5A.create(dataSetID, "unit", UCharTypeID, unitSpaceID);
							H5A.write<char>(label, UCharTypeID, new H5Array<char>(Scope.OnTrace[TIndex].TraceUnit.ToCharArray()));
							H5D.write<double>(dataSetID, doubleTypeId, new H5Array<double>(Scope.OnTrace[TIndex].Data));
						}
					}
				}

			} else {
				for (int SIndex = 1; SIndex < SList.Count; SIndex++) {
					ScopeClass Scope = SList[SIndex];
					string groupIDString = string.Format("Oscilloscope{0:d}", SIndex + 1);
					H5GroupId groupID = H5G.create(fileID, groupIDString);

					long[] dataDimension = new long[2];
					dataDimension[0] = Scope.OnTrace.Count+1;
					dataDimension[1] = Scope.DataLength;
					H5DataSpaceId dataSpaceId = H5S.create_simple(1, dataDimension);
					H5DataTypeId doubleTypeId = H5T.copy(H5T.H5Type.NATIVE_DOUBLE);

					H5T.setOrder(doubleTypeId, H5T.Order.LE);
					long [] labelDimension = new long[1];
					long [] unitDimension = new long[1];

					for (int TIndex = 0; TIndex < Scope.OnTrace.Count; TIndex++) {

						labelDimension[0] = Scope.ChannelLabel(TIndex).Length;
						H5DataSpaceId labelSpaceID = H5S.create_simple(1, labelDimension);
						unitDimension[0] = Scope.ChannelUnit(TIndex).Length;
						H5DataSpaceId unitSpaceID = H5S.create_simple(1, unitDimension);
						
						H5DataSetId dataSetID = H5D.create(fileID, "/" + Scope.ChannelLabel(TIndex) , doubleTypeId, dataSpaceId);
						H5AttributeId label = H5A.create(dataSetID, "label",  UCharTypeID, labelSpaceID);
						H5A.write<char>(label,UCharTypeID,new H5Array<char>(Scope.OnTrace[TIndex].TraceLabel.ToCharArray()));
						H5AttributeId unit = H5A.create(dataSetID, "unit", UCharTypeID, unitSpaceID);
						H5A.write<char>(label, UCharTypeID, new H5Array<char>(Scope.OnTrace[TIndex].TraceUnit.ToCharArray()));
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
