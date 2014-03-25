using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HDF5DotNet;

namespace SpiderU {
	class HDF5WriterClass: FileWriterClass {


		HDF5WriterClass(string FileName) : base(FileName) {

		}

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

		public override void WriteFile() {

		}

	}
}
