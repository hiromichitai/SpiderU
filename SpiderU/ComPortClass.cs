using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NationalInstruments.NI4882;
using TmctlAPINet;
using System.Runtime.InteropServices;
using System.Collections;
using LibUsbDotNet;
using LibUsbDotNet.Main;


namespace SpiderU {
	public class OscilloUSBPhyClass {
		private Int16 VendorIDValue;
		private Int16 ProductIDValue;
		private int NumberofChannel;
		private string VendorStringValue;
		private string ModelStringValue;
		public OscilloUSBPhyClass(Int16 VID, Int16 PID, int NChannel, string VString, string MString) {
			VendorIDValue = VID;
			ProductIDValue = PID;
			NumberofChannel = NChannel;
			VendorStringValue = VString;
			ModelStringValue = MString;
		}

		public Int16 VendorID {
			get {
				return VendorIDValue;
			}
		}

		public Int16 ProductID {
			get {
				return ProductIDValue;
			}
		}

		public int NumChannel {
			get {
				return NumberofChannel;
			}
		}

		public string VendorString {
			get {
				return VendorStringValue;
			}
		}

		public string ModelString {
			get {
				return ModelStringValue;
			}
		}
	}

	public class OscilloUSBPhyListClass {
		private static readonly OscilloUSBPhyListClass instance = new OscilloUSBPhyListClass();

		private static List<OscilloUSBPhyClass> OscilloUSBPhyList;

		private OscilloUSBPhyListClass() {
			OscilloUSBPhyList = new List<OscilloUSBPhyClass>();
			OscilloUSBPhyList.Add(new OscilloUSBPhyClass(0x5345, 0x1234,2,"OWON","PDS5022"));
		}

		public static List<OscilloUSBPhyClass> OscilloscopeList {
			get{
				return OscilloUSBPhyList;
			}
		}
	
	} 


 
	public class ComPortClass { // ComDeviceClass is a base class of communication by GPIB or USB
		public enum DeviceTypeEnum  { GPIB = 0, USBTMC = 1, USBPHY = 2}
		private DeviceTypeEnum DeviceTypeValue;
		private Device GPIBDevice;
		private byte GPIBPrimaryAddress;
		private byte GPIBSecondaryAddress;
		private TMCTL TMCTLDevice;
		private int TMCTLDeviceID;
		private string TMCTLDeviceSerial;
		private string IDStringValue;
		private string VendorStringValue;
		private string ModelStringValue;
		private Int16 VendorIDValue;
		private Int16 ProductIDValue;
		private int NumChannel;
		private OscilloUSBPhyClass OscilloUSBPhy;
		private UsbDevice USBDevice;
		private UsbEndpointReader USBEPReader;
		private UsbEndpointWriter USBEPWriter;

		public ComPortClass(DeviceTypeEnum DeviceType, string Serial) {
			if (DeviceType != DeviceTypeEnum.USBTMC) {
				ErrorDialog EDialog = new ErrorDialog("UIMSGINTILLEGALARG", " in ComPortClass");
				return;
			}
			DeviceTypeValue = DeviceType;
			TMCTLDeviceSerial = Serial;
		}

		public ComPortClass(DeviceTypeEnum DeviceType, byte PrimaryAddress, byte SecondaryAddress) {
			if (DeviceType != DeviceTypeEnum.GPIB) {
				ErrorDialog EDialog = new ErrorDialog("UIMSGINTILLEGALARG", " in ComDeviceClass");
				return;
			}
			DeviceTypeValue = DeviceType;
			GPIBPrimaryAddress = PrimaryAddress;
			GPIBSecondaryAddress = SecondaryAddress;
		}

		public ComPortClass(DeviceTypeEnum DeviceType, Int16 VendorID, Int16 ProductID, string VendorString, string ModelString) {
			if (DeviceType != DeviceTypeEnum.USBPHY) {
				ErrorDialog EDialog = new ErrorDialog("UIMSGINTILLEGALARG", " in ComDeviceClass");
				return;
			}
			DeviceTypeValue = DeviceType;
			VendorIDValue = VendorID;
			ProductIDValue = ProductID;
			VendorStringValue = VendorString;
			ModelStringValue = ModelString;
		}

		private void MakeIDString(string IDNString) { // split vendor and model string from *IDN? query
			string[] IDStrings = IDNString.Split(',');
			switch (IDStrings[0]) {

				case ("YOKOGAWA"):
					VendorStringValue = "YOKOGAWA";
					switch (IDStrings[1]) {
						case ("701500"):		// DL1540
						case ("701520"):		// DL1540L
						case ("701530"):		// DL1540C
						case ("701540"):		// DL1540CL
							ModelStringValue = "DL1540";
							NumChannel = 4;
							break;
						case ("701505"):		// DL1520
						case ("701515"):		// DL1520L
							ModelStringValue = "DL1520";
							NumChannel = 2;
							break;
						case ("701605"):		// DL1620
							ModelStringValue = "DL1620";
							NumChannel = 2;
							break;
						case ("701610"):		// DL1640
						case ("701620"):		// DL1640L
							ModelStringValue = "DL1640";
							NumChannel = 4;
							break;
						case ("701705"):		// DL1720
						case ("701715"):		// DL1720E
						case ("701725"):		// DL1735E
							ModelStringValue = "DL1720";
							NumChannel = 2;
							break;
						case ("701710"):		// DL1740
						case ("701730"):		// DL1740E
						case ("701740"):		// DL1740EL
						case ("701680"):		// DL1740EL?
							ModelStringValue = "DL1740";
							NumChannel = 4;
							break;
						case ("700410"):		// DL4080?
							ModelStringValue = "DL4080";
							NumChannel = 4;
							break;
						case ("701210"):		// DL750
						case ("701230"):		// DL750P
							ModelStringValue = "DL750";
							NumChannel = 4;
							break;
						case ("DL850"):		// DL850
						case ("DL850V"):		// DL850
							ModelStringValue = "DL850";
							NumChannel = 4;
							break;
						case ("701820"):		// DL708E
							ModelStringValue = "DL708";
							NumChannel = 8;
							break;
						case ("701830"):		// DL716
						case ("701831"):		// DL716
							ModelStringValue = "DL716";
							NumChannel = 16;
							break;
						case ("710105"):		// DLM2022
							ModelStringValue = "DLM2022";
							NumChannel = 2;
							break;
						case ("710115"):		// DLM2032
							ModelStringValue = "DLM2032";
							NumChannel = 2;
							break;
						case ("710125"):		// DLM2052
							ModelStringValue = "DLM2052";
							NumChannel = 2;
							break;
						case ("710110"):		// DLM2024
							ModelStringValue = "DLM2024";
							NumChannel = 4;
							break;
						case ("710120"):		// DLM2034
							ModelStringValue = "DLM2034";
							NumChannel = 4;
							break;
						case ("710130"): 	// DLM2054	
							ModelStringValue = "DLM2054";
							NumChannel = 4;
							break;
					}
					break;
				case ("*IDN LECROY"):
					VendorStringValue = "LECROY";
					switch (IDStrings[1]) {
						case (""):
							break;

					}
					break;
				default:
					VendorStringValue = "Unknown";
					ModelStringValue = "Unknown";
					break;

			}
			IDStringValue = VendorStringValue + " " + ModelStringValue;
		}


		public DeviceTypeEnum DeviceType {
			get {
				return DeviceTypeValue;
			}
		}

		private void GetIDString(){
			switch(DeviceTypeValue){
				case(DeviceTypeEnum.GPIB):
					GPIBDevice.Write("*IDN?");
					string IDNString = GPIBDevice.ReadString();
					MakeIDString(IDNString);
					IDStringValue = IDStringValue + "(" + GPIBDevice.PrimaryAddress.ToString() + ")";
					break;
				case(DeviceTypeEnum.USBTMC):
					TMCTLDevice.Send(TMCTLDeviceID, "*IDN?");
					string RBuffer = "";
					int RLength = 0;
					TMCTLDevice.Receive(TMCTLDeviceID, ref RBuffer, ref RLength);
					MakeIDString(RBuffer);
					IDStringValue = IDStringValue + "(" + TMCTLDeviceID.ToString() + ")";
					break;
				case (DeviceTypeEnum.USBPHY):
					VendorStringValue = OscilloUSBPhy.VendorString;
					ModelStringValue = OscilloUSBPhy.ModelString;
					IDStringValue = VendorStringValue + " " + ModelStringValue + "(" +  ")";
					break;
				
			}
		}


		public void InitializeComPort() {	// initialize communication device
			switch (DeviceTypeValue) {
				case (DeviceTypeEnum.GPIB):
					GPIBDevice = new Device(0, GPIBPrimaryAddress, GPIBSecondaryAddress);
					break;
				case (DeviceTypeEnum.USBTMC):
					TMCTLDevice = new TMCTL();
					TMCTLDevice.Initialize(TMCTL.TM_CTL_USBTMC2, TMCTLDeviceSerial, ref TMCTLDeviceID);
					break;
				case (DeviceTypeEnum.USBPHY):
					OscilloUSBPhy = new OscilloUSBPhyClass(VendorIDValue, ProductIDValue, NumChannel, VendorStringValue, ModelStringValue);
					UsbDeviceFinder UsbFinder = new UsbDeviceFinder(OscilloUSBPhy.VendorID,OscilloUSBPhy.ProductID);
					USBDevice = UsbDevice.OpenUsbDevice(UsbFinder);
					if (USBDevice == null) {
						ErrorDialog EDialog = new ErrorDialog("UIMSGCANTOPENUSBPHY", OscilloUSBPhy.VendorString);
						return;
					}
					const int DescriptorLength = 512;
					int ReceiveLength = 0;
					byte[] ConfigDesciptor = new byte[DescriptorLength];

					GCHandle DescritprArray = GCHandle.Alloc(DescriptorLength, GCHandleType.Pinned);
					IntPtr DescriptorPtr = DescritprArray.AddrOfPinnedObject();

					USBDevice.GetDescriptor((byte)LibUsbDotNet.Descriptors.DescriptorType.Device,0,0,DescriptorPtr,DescriptorLength,out ReceiveLength);
					USBDevice.GetDescriptor((byte)LibUsbDotNet.Descriptors.DescriptorType.Configuration,0,0,DescriptorPtr,DescriptorLength,out ReceiveLength);
					Marshal.Copy(DescriptorPtr, ConfigDesciptor, 0, ReceiveLength);
					int DescriptorIndex = 0;
					byte[] EPAddress = null;
					int EPIndex = 0;
					while (DescriptorIndex < ReceiveLength) {
						byte DTSize = ConfigDesciptor[DescriptorIndex];
						byte DType = ConfigDesciptor[DescriptorIndex+1];
						if (DType == 0x04) {  // Interface descriptor 
							byte NumEndpoint = ConfigDesciptor[DescriptorIndex + 4];
							EPAddress = new byte[NumEndpoint];
							EPIndex = 0;
						}
						if (DType == 0x05) { // Endpoint descriptor
							EPAddress[EPIndex] = ConfigDesciptor[DescriptorIndex + 2];
							EPIndex++;
						}
						DescriptorIndex += DTSize;
					}
		
					DescritprArray.Free();

					IUsbDevice wholeUsbDevice = USBDevice as IUsbDevice;
					if (!ReferenceEquals(wholeUsbDevice, null)) {
						wholeUsbDevice.SetConfiguration(1);
						wholeUsbDevice.ClaimInterface(0);
					}
					if (EPAddress.Length != 2) { // PDS series should have two Endpoint
						ErrorDialog EDialog = new ErrorDialog("UIMSGILLEGALNUMEP", " in InitializeComPort");
					}
					if (EPAddress[0] > 0x80) { // IN EP
						USBEPReader = USBDevice.OpenEndpointReader((ReadEndpointID)EPAddress[0]);
					} else {
						USBEPWriter = USBDevice.OpenEndpointWriter((WriteEndpointID)EPAddress[0]);

					}
					if (EPAddress[1] > 0x80) { // IN EP
						USBEPReader = USBDevice.OpenEndpointReader((ReadEndpointID)EPAddress[1]);
					} else {
						USBEPWriter = USBDevice.OpenEndpointWriter((WriteEndpointID)EPAddress[1]);

					}
					USBEPReader.Reset();
					USBEPWriter.Reset();

					break;
			}
			GetIDString();
		}

		public void Close() {	// close communication port
			switch (DeviceTypeValue) {
				case (DeviceTypeEnum.GPIB):
					if (GPIBDevice != null) {
						GPIBDevice.GoToLocal();
						GPIBDevice.Dispose();
					}
					break;
				case (DeviceTypeEnum.USBTMC):
					if (TMCTLDevice != null) {
						if (TMCTLDeviceID >= 0) {
							TMCTLDevice.Finish(TMCTLDeviceID);
						}
					}
					break;
				case (DeviceTypeEnum.USBPHY):
					if (USBEPReader != null) {
						USBEPReader.Dispose();
					}
					if (USBEPWriter != null) {
						USBEPWriter.Dispose();
					}
					if (USBDevice != null) {
						USBDevice.Close();
					}
					break;
			}
		}


		public string VendorString {
			get {
				return VendorStringValue;
			}
		}

		public string ModelString {
			get {
				return ModelStringValue;
			}
		}

		public string IDString {
			get {
				return IDStringValue;
			}
		}

		public int NChannel {
			get{
				return NumChannel;
			}
		}

		public void Write(string WriteString) {
			switch(DeviceTypeValue){
				case(DeviceTypeEnum.GPIB):
					GPIBDevice.Write(WriteString);
					break;
				case(DeviceTypeEnum.USBTMC):
					TMCTLDevice.Send(TMCTLDeviceID, WriteString);
					break;
				case (DeviceTypeEnum.USBPHY):
					Encoding enc = Encoding.GetEncoding("us-ascii");
					byte[] WriteBuffer = enc.GetBytes(WriteString);
					int BufferLength = WriteBuffer.Length;
					USBEPWriter.Write(WriteBuffer, 3000, out BufferLength);
					break;

			}
		}

		public string ReadString() {
			const int BufferLength = 100;
			switch (DeviceTypeValue) {
				case (DeviceTypeEnum.GPIB):
					return GPIBDevice.ReadString();
				case (DeviceTypeEnum.USBTMC):
					string RBuffer = "";
					int RLength = 0;
					TMCTLDevice.Receive(TMCTLDeviceID, ref RBuffer, ref RLength);
					return RBuffer;
				case (DeviceTypeEnum.USBPHY):
					byte[] ReadBuffer = new byte[BufferLength];
					int ReadLength = 0;
					ErrorCode ecode = USBEPReader.Read(ReadBuffer, 3000, out ReadLength);
					if (ecode != ErrorCode.None) {
						WarningDialog WDialog = new WarningDialog("UIMSGUSBREADERROR", "ReadString");
						return null;
					}
					break;
			}
			return null;
		}


		public unsafe byte[] ReadByteArray(int NumByte) {
			const int BufferLength = 1000;
			switch (DeviceTypeValue) {
				case (DeviceTypeEnum.GPIB):
					return GPIBDevice.ReadByteArray(NumByte);
				case (DeviceTypeEnum.USBTMC):
					byte* BufferPointer = (byte*)Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(byte)) * NumByte);
					int endflag = 0;
					int NumReceived = 0;
					int result = TMCTLDevice.ReceiveBlockData(TMCTLDeviceID, ref *BufferPointer, NumByte, ref NumReceived, ref endflag);
					byte [] ByteBuffer = new byte[NumByte];
					Marshal.Copy((System.IntPtr)BufferPointer,ByteBuffer , 0, NumByte);
					Marshal.FreeCoTaskMem((IntPtr)BufferPointer);
					return ByteBuffer;
				case (DeviceTypeEnum.USBPHY):
					byte[] ReadBuffer = new byte[NumByte];
					int ReadLength = 0;
					ErrorCode ecode = USBEPReader.Read(ReadBuffer, 3000, out ReadLength);
					if (ecode != ErrorCode.None) {
						WarningDialog WDialog = new WarningDialog("UIMSGUSBREADERROR", "ReadString");
						return null;
					}
					break;
			}
			return null;
		}

		public void GoToLocal() {
			switch (DeviceTypeValue) {
				case (DeviceTypeEnum.GPIB):
					GPIBDevice.GoToLocal();
					break;
				case (DeviceTypeEnum.USBTMC):
					TMCTLDevice.SetRen(TMCTLDeviceID, 0);
					break;
			}
		}
	}
}
