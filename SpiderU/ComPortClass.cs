﻿using System;
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
using LibUsbDotNet.Info;
using LibUsbDotNet.Descriptors;
using MonoLibUsb;


namespace SpiderU {
	public class OscilloUSBPhyClass {
		private Int16 VendorIDValue;
		private Int16 ProductIDValue;
		private string VendorStringValue;
		private string ModelStringValue;
		private byte WriteEPAddress;
		private byte ReadEPAddress;

		public OscilloUSBPhyClass(Int16 VID, Int16 PID) {
			VendorIDValue = VID;
			ProductIDValue = PID;
			VendorStringValue = OscilloUSBPhyListClass.GetVendorString(VID,PID);
			ModelStringValue = OscilloUSBPhyListClass.GetModelString(VID, PID);
			WriteEPAddress = OscilloUSBPhyListClass.GetWriteEPAddress(VID, PID);
			ReadEPAddress = OscilloUSBPhyListClass.GetReadEPAddress(VID, PID);
		}

		public OscilloUSBPhyClass(Int16 VID, Int16 PID, string VString, string MString, byte WEndpoint, byte REndpoint) {
			VendorIDValue = VID;
			ProductIDValue = PID;
			VendorStringValue = VString;
			ModelStringValue = MString;
			WriteEPAddress = WEndpoint;
			ReadEPAddress = REndpoint;
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

		public byte WriteEndpoint {
			get {
				return WriteEPAddress;
			}
		}

		public byte ReadEndpoint {
			get {
				return ReadEPAddress;
			}
		}

	}

	public class OscilloUSBPhyListClass {
		private static readonly OscilloUSBPhyListClass instance = new OscilloUSBPhyListClass();

		private static List<OscilloUSBPhyClass> OscilloUSBPhyList;

		private OscilloUSBPhyListClass() {
			OscilloUSBPhyList = new List<OscilloUSBPhyClass>();
			OscilloUSBPhyList.Add(new OscilloUSBPhyClass(0x5345, 0x1234,"OWON","PDS5022",0x03,0x81));
		}

		public static List<OscilloUSBPhyClass> OscilloscopeList {
			get{
				return OscilloUSBPhyList;
			}
		}

		public static string GetVendorString(Int16 VID, Int16 PID) {
			OscilloUSBPhyClass USBOscillo = OscilloUSBPhyList.Find((Oscillo) => ((Oscillo.VendorID == VID) && (Oscillo.ProductID == PID)));
			return USBOscillo.VendorString;
		}

		public static string GetModelString(Int16 VID, Int16 PID) {
			OscilloUSBPhyClass USBOscillo = OscilloUSBPhyList.Find((Oscillo) => ((Oscillo.VendorID == VID) && (Oscillo.ProductID == PID)));
			return USBOscillo.ModelString;
		}

		public static byte GetWriteEPAddress(Int16 VID, Int16 PID) {
			OscilloUSBPhyClass USBOscillo = OscilloUSBPhyList.Find((Oscillo) => ((Oscillo.VendorID == VID) && (Oscillo.ProductID == PID)));
			return USBOscillo.WriteEndpoint;
		}

		public static byte GetReadEPAddress(Int16 VID, Int16 PID) {
			OscilloUSBPhyClass USBOscillo = OscilloUSBPhyList.Find((Oscillo) => ((Oscillo.VendorID == VID) && (Oscillo.ProductID == PID)));
			return USBOscillo.ReadEndpoint;
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
		private static DateTime LastDataEventDate;

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
				case ("LECROY"):
					VendorStringValue = "LECROY";
					switch (IDStrings[1]) {
						case ("WR6050A"):
							ModelStringValue = "WR6050A";
							NumChannel = 4;
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
					if (IDNString.Contains("*IDN")) {
						IDNString = IDNString.Replace("*IDN","");
						IDNString = IDNString.Trim();
					}
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


		public unsafe void InitializeComPort() {	// initialize communication device
			switch (DeviceTypeValue) {
				case (DeviceTypeEnum.GPIB):
					GPIBDevice = new Device(0, GPIBPrimaryAddress, GPIBSecondaryAddress);
					break;
				case (DeviceTypeEnum.USBTMC):
					TMCTLDevice = new TMCTL();
					TMCTLDevice.Initialize(TMCTL.TM_CTL_USBTMC2, TMCTLDeviceSerial, ref TMCTLDeviceID);
					break;
				case (DeviceTypeEnum.USBPHY):
					OscilloUSBPhy = new OscilloUSBPhyClass(VendorIDValue, ProductIDValue);
					UsbDeviceFinder UsbFinder = new UsbDeviceFinder(OscilloUSBPhy.VendorID,OscilloUSBPhy.ProductID);
					USBDevice = UsbDevice.OpenUsbDevice(UsbFinder);

					if (USBDevice == null) {
						ErrorDialog EDialog = new ErrorDialog("UIMSGCANTOPENUSBPHY", OscilloUSBPhy.VendorString);
						return;
					}

					IUsbDevice wholeUsbDevice = USBDevice as IUsbDevice;
					if (!ReferenceEquals(wholeUsbDevice, null)) {
						wholeUsbDevice.SetConfiguration(1);
						wholeUsbDevice.ClaimInterface(0);
					}

					USBEPReader = USBDevice.OpenEndpointReader((ReadEndpointID)OscilloUSBPhy.ReadEndpoint);
					USBEPWriter = USBDevice.OpenEndpointWriter((WriteEndpointID)OscilloUSBPhy.WriteEndpoint);

					break;
			}
			GetIDString();
		}

		public void ResetEPReader() {	// reset EPReader of ComPort
			if (DeviceTypeValue != DeviceTypeEnum.USBPHY) {
				ErrorDialog EDialog = new ErrorDialog("UIMSGINTILLEGALARG", " in ResetEPReader");
				return;
			} else {
				USBEPReader.Reset();
			}
		}

		public void ResetEPWriter() {	// reset EPWriter of ComPort
			if (DeviceTypeValue != DeviceTypeEnum.USBPHY) {
				ErrorDialog EDialog = new ErrorDialog("UIMSGINTILLEGALARG", " in ResetEPReader");
				return;
			} else {
				USBEPWriter.Reset();
			}
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
		
					IUsbDevice wholeUsbDevice = USBDevice as IUsbDevice;
					if (!ReferenceEquals(wholeUsbDevice, null)) {
						wholeUsbDevice.ReleaseInterface(USBEPReader.EpNum);
						wholeUsbDevice.ReleaseInterface(USBEPWriter.EpNum);
					}

					if (USBDevice != null) {
						USBDevice.Close();
						USBDevice = null;
					}
					UsbDevice.Exit();
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
					int BufferLength;
					USBEPWriter.Write(WriteBuffer, 3000, out BufferLength);
					if (BufferLength != WriteBuffer.Length) {
						ErrorDialog EDialog = new ErrorDialog("UIMSGUSBWRITEERROR", " in Wrte of ComPortClass");
					}
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

		private static void OnRxEndPointData(object sender, EndpointDataEventArgs e) {
			LastDataEventDate = DateTime.Now;
			Console.Write(Encoding.Default.GetString(e.Buffer, 0, e.Count));
		}
 
		public unsafe byte[] ReadByteArray(int NumByte) {
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
					ErrorCode ecode = USBEPReader.Read(ReadBuffer, 0, NumByte, 3000, out ReadLength);
					if (ecode != ErrorCode.None) {
						WarningDialog WDialog = new WarningDialog("UIMSGUSBREADERROR", "ReadString");
						return null;
					}
					return ReadBuffer;
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
