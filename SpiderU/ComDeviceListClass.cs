﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NationalInstruments.NI4882;
using TmctlAPINet;
using System.Windows;
using System.Management;
using LibUsbDotNet;
using LibUsbDotNet.Main;


namespace SpiderU {

	public class ComDeviceListClass {
		const byte PRIMARYADDRESSLOWEST = 1;
		const byte PRIMARYADDRESSHIGHEST = 16;
		

		private Board GPIBBoard;
        private AddressCollection FreeGPIBDeviceAddressList;
		private bool HasGPIB;
		private bool HasUSBTMC;
		private TmctlAPINet.DEVICELIST [] USBTMCDeviceList;
		private const int MaxNumUSBScope = 10;
		private int NumUSBScope;
		private TMCTL TMCTLInstance;
		private List<ComDeviceClass> FreeDeviceList;

        public ComDeviceListClass() {
			try {
				FreeDeviceList = new List<ComDeviceClass>();
				GPIBBoard = new Board();
				GPIBBoard.SendInterfaceClear();
				FreeGPIBDeviceAddressList = new AddressCollection();
				for (byte PAddress = PRIMARYADDRESSLOWEST; PAddress < PRIMARYADDRESSHIGHEST; PAddress++) {
					FreeGPIBDeviceAddressList.Add(new Address(PAddress));
				}
				FreeGPIBDeviceAddressList = GPIBBoard.FindListeners(FreeGPIBDeviceAddressList);
				GPIBBoard.Dispose();
				for (int GIndex = 0; GIndex < FreeGPIBDeviceAddressList.Count; GIndex++) {
					ComDeviceClass NewComDevice =
						new ComDeviceClass(ComDeviceClass.DeviceTypeEnum.GPIB, 
							FreeGPIBDeviceAddressList[GIndex].PrimaryAddress,FreeGPIBDeviceAddressList[GIndex].SecondaryAddress);
					FreeDeviceList.Add(NewComDevice);
				}
				HasGPIB = true;
			}
			catch (System.DllNotFoundException) {
				HasGPIB = false;
			}
			catch (System.EntryPointNotFoundException) {
				HasGPIB = false;
			}
			catch (NationalInstruments.NI4882.GpibException) {
				HasGPIB = false;
			}
			catch (System.ArgumentException) {
				HasGPIB = false;
			}
			catch (System.Exception Ex) {
				throw (Ex);
			}

			try {	// search for USB TMC oscilloscope
				TMCTLInstance = new TMCTL();
				USBTMCDeviceList = new TmctlAPINet.DEVICELIST[MaxNumUSBScope];
				int result = TMCTLInstance.SearchDevices(TMCTL.TM_CTL_USBTMC2, USBTMCDeviceList, MaxNumUSBScope, ref NumUSBScope,  "");
				if (result == 0) {
					if (NumUSBScope > 0) {
						for (int UOIndex = 0; UOIndex < NumUSBScope; UOIndex++) {
							string EncodedSerial = USBTMCDeviceList[UOIndex].ToString();
							ComDeviceClass NewComDevice =
								new ComDeviceClass(ComDeviceClass.DeviceTypeEnum.USBTMC,EncodedSerial);
							FreeDeviceList.Add(NewComDevice);
						}
					}
					HasUSBTMC = true;
				}
			}
			catch (System.NullReferenceException) {
				HasUSBTMC = false;
			}
			catch (System.DllNotFoundException) {
				HasUSBTMC = false;
			}
			catch (System.Exception Ex) {
				throw (Ex);
			}


			try { // search for USB PHY class oscilloscope
				foreach (OscilloUSBPhyClass OscilloUSBPhy in OscilloUSBPhyListClass.OscilloscopeList) {
					UsbDeviceFinder UsbFinder = new UsbDeviceFinder(OscilloUSBPhy.VendorID,OscilloUSBPhy.ProductID);
					UsbDevice OscilloUsbDevice = UsbDevice.OpenUsbDevice(UsbFinder);
					if (OscilloUsbDevice != null) {
						ComDeviceClass NewComDevice =
							new ComDeviceClass(ComDeviceClass.DeviceTypeEnum.USBPHY, OscilloUSBPhy.VendorID, OscilloUSBPhy.ProductID,OscilloUSBPhy.VendorString,OscilloUSBPhy.ModelString);
						FreeDeviceList.Add(NewComDevice);
						OscilloUsbDevice.Close();
					}
				}
			}
			catch (System.Exception Ex) {
				throw (Ex);
			}

        }

		public ComDeviceClass this[int Index] {
			get{
				return FreeDeviceList[Index];
			}
		}

        public int NumDevice() {
            return FreeDeviceList.Count;
        }

		public int NumFreeDevice() {
			return FreeDeviceList.Count;
		}

        public void GetGPIBDeviceList() {
        }

		public void UseDevice(ComDeviceClass ComDevice) {
			if (FreeDeviceList.Count > 0) {
				for (int Index = 0; Index < FreeDeviceList.Count; Index++) {
					if(ComDevice == FreeDeviceList[Index]){
						FreeDeviceList.RemoveAt(Index);
					}
				}
			}
		}


        public byte FreeDevicePrimaryAddress(int Index) {
            return FreeGPIBDeviceAddressList[Index].PrimaryAddress;
        }

		public byte FreeDeviceSecondaryAddress(int Index) {
            return FreeGPIBDeviceAddressList[Index].SecondaryAddress;
        }

 
    }
}
