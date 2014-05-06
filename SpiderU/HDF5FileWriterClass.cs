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

/*
            // Create the data set.
            H5DataSetId dataSetId = H5D.create(fileId, "/csharpExample",
                                               typeId, spaceId);

            H5D.write(dataSetId, new H5DataTypeId(H5T.H5Type.NATIVE_INT),
                              new H5Array<int>(dset_data));


            // Close all the open resources.
            H5D.close(dataSetId);

	
		// Function used with H5L.iterate
		static H5IterationResult MyH5LFunction(H5GroupId id,
											   string objectName,
											   H5LinkInfo info, Object param) {
			Console.WriteLine("The object name is {0}", objectName);
			Console.WriteLine("The linkType is {0}", info.linkType);
			Console.WriteLine("The object parameter is {0}", param);
			return H5IterationResult.SUCCESS;
		}

		// Function used with H5A.iterate
		static H5IterationResult MyH5AFunction(
		   H5AttributeId attributeId,
		   String attributeName,
		   H5AttributeInfo info,
		   Object attributeNames) {
			Console.WriteLine("Iteration attribute is {0}", attributeName);

			return H5IterationResult.SUCCESS;
		}
*/

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
					dimension[0] = Scope.NumOnChannel+1;
					dimension[1] = Scope.DataLength;
					H5DataSpaceId spaceId = H5S.create_simple(2, dimension);
					H5DataTypeId typeId = H5T.copy(H5T.H5Type.NATIVE_DOUBLE);

					int typeSize = H5T.getSize(typeId);
					H5T.setOrder(typeId, H5T.Order.LE);

					H5DataSetId dataSetId = H5D.create(fileID, "/"+groupIDString, typeId, spaceId);
	
					// H5Array<double>

					// Write the integer data to the data set.
					/*
					H5D.write(dataSetId, new H5DataTypeId(H5T.H5Type.NATIVE_INT),
									  new H5Array<int>(dset_data));

					H5D.close(dataSetId);
					H5G.close(groupId);
					 */
				}

			}

		}

		public override void Close() {

		}

	}
}
