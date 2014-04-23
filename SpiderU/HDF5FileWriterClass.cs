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
				WarningDialog WDialog = new WarningDialog("UIMSGARGEXCEPTION", "in CSVFileWriter");
			}
			catch (System.Security.SecurityException) {
				WarningDialog WDialog = new WarningDialog("UIMSGSECURITYEXCEPTION", "in CSVFileWriter");
			}

		}

/*
            H5GroupId groupId = H5G.create(fileId, "/cSharpGroup");
            H5GroupId subGroup = H5G.create(groupId, "mySubGroup");
            H5G.close(subGroup);

            // Prepare to create a data space for writing a 1-dimensional 
            // signed integer array.
            long[] dims = new long[RANK];
            dims[0] = DATA_ARRAY_LENGTH;

            H5DataSpaceId spaceId = H5S.create_simple(RANK, dims);
            H5DataTypeId typeId = H5T.copy(H5T.H5Type.NATIVE_INT);

            // Find the size of the type
            int typeSize = H5T.getSize(typeId);

            // Set the order to big endian
            H5T.setOrder(typeId, H5T.Order.BE);

            // Set the order to little endian
            H5T.setOrder(typeId, H5T.Order.LE);

            // Create the data set.
            H5DataSetId dataSetId = H5D.create(fileId, "/csharpExample",
                                               typeId, spaceId);

            // Write the integer data to the data set.
            H5D.write(dataSetId, new H5DataTypeId(H5T.H5Type.NATIVE_INT),
                              new H5Array<int>(dset_data));

            // If we were writing a single value it might look like this.
            //  int singleValue = 100;
            //  H5D.writeScalar(dataSetId, 
            //                 new H5DataTypeId(H5T.H5Type.NATIVE_INT),
            //                 ref singleValue);

            // Create an integer array to receive the read data.
            int[] readDataBack = new int[DATA_ARRAY_LENGTH];

            // Read the integer data back from the data set
            H5D.read(dataSetId, new H5DataTypeId(H5T.H5Type.NATIVE_INT),
                new H5Array<int>(readDataBack));

            // Echo the data
            for (int i = 0; i < DATA_ARRAY_LENGTH; i++)
            {
               Console.WriteLine(readDataBack[i]);
            }

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
			H5GroupId groupID = H5G.create(fileID, "/cSharpGroup");
			H5GroupId subGroup = H5G.create(groupID, "mySubGroup");
			H5G.close(subGroup);

//			long[] dims = new long[RANK];
//			dims[0] = DATA_ARRAY_LENGTH;

//			H5DataSpaceId spaceId = H5S.create_simple(RANK, dims);
			H5DataTypeId typeId = H5T.copy(H5T.H5Type.NATIVE_INT);

		}

		public override void Close() {

		}

	}
}
