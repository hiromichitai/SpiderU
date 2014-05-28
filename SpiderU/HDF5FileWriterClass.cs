using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HDF5DotNet;

namespace SpiderU {
	class HDF5FileWriterClass: FileWriterClass {
		private H5FileId fileID;

		HDF5FileWriterClass(string FileName) : base(FileName) {
			try {
				fileID = H5F.create(FileName, H5F.CreateMode.ACC_CREAT);
			}
			catch (ArgumentException) {
				WarningDialog WDialog = new WarningDialog("UIMSGARGEXCEPTION", "in HDF5FileWriter");
			}
			catch (System.Security.SecurityException) {
				WarningDialog WDialog = new WarningDialog("UIMSGSECURITYEXCEPTION", "in HDF5FileWriter");
			}

		}


		public override void WriteFile() {
			List<ScopeClass> SList = ScopeManager.ScopeList;
			if (Properties.Settings.Default.syncAllScope) {
				if (Properties.Settings.Default.addComment) {
					string GrooupComment = SList[0].Comment;
					for (int SIndex = 1; SIndex < SList.Count; SIndex++) {
						GrooupComment = GrooupComment + SList[SIndex].Comment;
					}
				}
			} else {
				for (int SIndex = 1; SIndex < SList.Count; SIndex++) {
					ScopeClass Scope = SList[SIndex];
					string groupIDString = string.Format("Oscilloscope{0:d}", SIndex + 1);
					H5GroupId groupID = H5G.create(fileID, groupIDString);

					long[] dimension = new long[2];
					dimension[0] = Scope.OnTrace.Count+1;
					dimension[1] = Scope.DataLength;
					H5DataSpaceId spaceId = H5S.create_simple(1, dimension);
					H5DataTypeId typeId = H5T.copy(H5T.H5Type.NATIVE_DOUBLE);

					H5T.setOrder(typeId, H5T.Order.LE);
					H5DataTypeId UCharTypeID = H5T.copy(H5T.H5Type.NATIVE_UCHAR);
					long [] labelDimension = new long[1];
					long [] unitDimension = new long[1];

					for (int TIndex = 0; TIndex < Scope.OnTrace.Count; TIndex++) {
						labelDimension[0] = Scope.ChannelLabel(TIndex).Length;
						H5DataSpaceId labelSpaceID = H5S.create_simple(1, labelDimension);
						unitDimension[0] = Scope.ChannelUnit(TIndex).Length;
						H5DataSpaceId unitSpaceID = H5S.create_simple(1, unitDimension);
						
						H5DataSetId dataSetID = H5D.create(fileID, "/" + Scope.ChannelLabel(TIndex) , typeId, spaceId);
						H5AttributeId label = H5A.create(dataSetID, "label",  UCharTypeID, labelSpaceID);

						/*
												H5A.write<char>();

													label, H5T.H5Type.NATIVE_UCHAR, new H5Array<char>(Scope.OnTrace[TIndex].TraceLabel));

												H5D.write(dataSetID, new H5Array<double>(Scope.OnTrace[TIndex].Data));

												H5D.close(dataSetID);
						*/
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
