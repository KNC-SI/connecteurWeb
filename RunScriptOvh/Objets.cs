//using MG2S_ClassLib.My;
using MG2S_ClassLib;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.ApplicationServices;
using Microsoft.VisualBasic.CompilerServices;
using Microsoft.VisualBasic.Devices;
using Microsoft.VisualBasic.MyServices;
using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Management;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

namespace RunScriptOvh
{
    public class Objets
    {
        public static bool StrCleanModeMySql;

        private static RSACryptoServiceProvider MyRSA;

        static Objets()
        {
            Objets.StrCleanModeMySql = false;
            Objets.MyRSA = new RSACryptoServiceProvider();
            //Objets.$STATIC$GetGeoCode$03EE22$GoogleOverQuery$Init = new StaticLocalInitFlag();
        }

        [DebuggerNonUserCode]
        public Objets()
        {
        }

        //public static string AppPath(bool pEndWithBackSlach = true)
        //{
        //    //string directoryPath = MyProject.Application.Info.DirectoryPath;
        //    if (!pEndWithBackSlach)
        //    {
        //        return directoryPath;
        //    }
        //    return string.Concat(directoryPath, "\\");
        //}

        public static string CleanNomChamp(string pNom)
        {
            string str = pNom.ToLower().Replace("é", "e").Replace("è", "e").Replace("ê", "e").Replace("ë", "e").Replace("î", "i").Replace("ï", "i").Replace("ô", "o").Replace("ö", "o").Replace("û", "u").Replace("ü", "u");
            string str1 = "";
            int length = checked(str.Length - 1);
            for (int i = 0; i <= length; i = checked(i + 1))
            {
                char chr = str[i];
                str1 = (!char.IsLetterOrDigit(chr) ? string.Concat(str1, "_") : string.Concat(str1, Conversions.ToString(chr)));
            }
            return str1.ToUpper();
        }

        public static string CleanNomFichier(string pNom)
        {
            char[] invalidPathChars = Path.GetInvalidPathChars();
            string str = pNom;
            int length = checked(checked((int)invalidPathChars.Length) - 1);
            for (int i = 0; i <= length; i = checked(i + 1))
            {
                str = str.Replace(Conversions.ToString(invalidPathChars[i]), "");
            }
            return str;
        }

        public static void CopyCol(Collection ColSrc, Collection ColDst)
        {
            IEnumerator enumerator = null;
            ColDst.Clear();
            try
            {
                enumerator = ColSrc.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    object objectValue = RuntimeHelpers.GetObjectValue(enumerator.Current);
                    ColDst.Add(RuntimeHelpers.GetObjectValue(objectValue), null, null, null);
                }
            }
            finally
            {
                if (enumerator is IDisposable)
                {
                    (enumerator as IDisposable).Dispose();
                }
            }
        }

        public static string Date2SQL(DateTime pDate, bool FormatAccess = false)
        {
            if (!FormatAccess)
            {
                return Strings.Format(pDate, "yyyyMMdd");
            }
            return string.Concat("#", Strings.Format(pDate, "MM/dd/yyyy"), "#");
        }

        public static void DGVLoadColsSize(Form pForm, DataGridView pDGV, string pAppName = "", bool pGlobal = false)
        {
            int i;
            string productName = pAppName;
            if (Operators.CompareString(productName, "", false) == 0)
            {
                productName = Application.ProductName;
            }
            string name = pForm.Name;
            string str = pDGV.Name;
            string str1 = string.Concat("Form", name, "DGV", str);
            if (!pGlobal)
            {
                int count = checked(pDGV.Columns.Count - 1);
                for (i = 0; i <= count; i = checked(i + 1))
                {
                    pDGV.Columns[i].Width = Conversions.ToInteger(Objets.LocalGetSetting(productName, str1, string.Concat("col", i.ToString()), pDGV.Columns[i].Width));
                }
            }
            else
            {
                int num = checked(pDGV.Columns.Count - 1);
                for (i = 0; i <= num; i = checked(i + 1))
                {
                    pDGV.Columns[i].Width = Conversions.ToInteger(Objets.GlobalGetSetting(productName, str1, string.Concat("col", i.ToString()), pDGV.Columns[i].Width, null));
                }
            }
        }

        public static void DGVSaveColsSize(Form pForm, DataGridView pDGV, string pAppName = "", bool pGlobal = false)
        {
            int i;
            string productName = pAppName;
            if (Operators.CompareString(productName, "", false) == 0)
            {
                productName = Application.ProductName;
            }
            string name = pForm.Name;
            string str = pDGV.Name;
            string str1 = string.Concat("Form", name, "DGV", str);
            if (!pGlobal)
            {
                int count = checked(pDGV.Columns.Count - 1);
                for (i = 0; i <= count; i = checked(i + 1))
                {
                    Objets.LocalSaveSetting(productName, str1, string.Concat("col", i.ToString()), pDGV.Columns[i].Width);
                }
            }
            else
            {
                int num = checked(pDGV.Columns.Count - 1);
                for (i = 0; i <= num; i = checked(i + 1))
                {
                    Objets.GlobalSaveSetting(productName, str1, string.Concat("col", i.ToString()), pDGV.Columns[i].Width, null);
                }
            }
        }

        public static string Double2SQL(double pVal)
        {
            string str = Conversion.Str(pVal);
            if (str.IndexOf(".") < 0)
            {
                str = string.Concat(str, ".0");
            }
            return str;
        }

        public static void ExtractRessource(string pResName, string pDirDest, Type GetTypeOfClassInAssemblyContainer = null)
        {
            Stream stream;
            stream = (GetTypeOfClassInAssemblyContainer != null ? Assembly.GetAssembly(GetTypeOfClassInAssemblyContainer).GetManifestResourceStream(pResName) : Assembly.GetExecutingAssembly().GetManifestResourceStream(pResName));
            if (stream == null)
            {
                throw new Exception(string.Concat("La ressource ", pResName, " n'existe pas."));
            }
            string str = Path.Combine(pDirDest, pResName.Substring(checked(pResName.IndexOf('.') + 1)));
            FileStream fileStream = File.Open(str, FileMode.Create);
            byte[] numArray = new byte[1025];
            int length = 1024;
            while (stream.Position < stream.Length)
            {
                if (checked(stream.Length - stream.Position) < (long)length)
                {
                    length = checked((int)(checked(stream.Length - stream.Position)));
                }
                stream.Read(numArray, 0, length);
                fileStream.Write(numArray, 0, length);
                fileStream.Flush();
            }
            stream.Close();
            fileStream.Close();
        }

        //public static string FileRead(string pNomFichier, bool pEmptyIfError = false, bool pForceCRLF = true, Encoding pEncoding = null, bool pAutoDetectEncoding = true)
        //{
        //    string str;
        //    try
        //    {
        //        string str1 = "";
        //        Encoding @default = Encoding.Default;
        //        if (pEncoding != null)
        //        {
        //            @default = pEncoding;
        //        }
        //        else if (pAutoDetectEncoding)
        //        {
        //            @default = Objets.GetFileEncoding(pNomFichier, null);
        //        }
        //        str1 = MyProject.Computer.FileSystem.ReadAllText(pNomFichier, @default);
        //        if (pForceCRLF && !str1.Contains("\r\n"))
        //        {
        //            if (str1.Contains("\r"))
        //            {
        //                str1 = str1.Replace("\r", "\r\n");
        //            }
        //            else if (str1.Contains("\n"))
        //            {
        //                str1 = str1.Replace("\n", "\r\n");
        //            }
        //        }
        //        str = str1;
        //    }
        //    catch (Exception exception)
        //    {
        //        ProjectData.SetProjectError(exception);
        //        if (!pEmptyIfError)
        //        {
        //            throw;
        //        }
        //        else
        //        {
        //            str = "";
        //            ProjectData.ClearProjectError();
        //        }
        //    }
        //    return str;
        //}

        public static long FileSize(string pFilename, bool pReturnZeroIfNotExist = false)
        {
            long length;
            try
            {
                length = (new FileInfo(pFilename)).Length;
            }
            catch (Exception exception)
            {
                ProjectData.SetProjectError(exception);
                if (!pReturnZeroIfNotExist)
                {
                    throw;
                }
                else
                {
                    length = (long)0;
                    ProjectData.ClearProjectError();
                }
            }
            return length;
        }

        public static void FileWrite(string pNomFichier, string pTexte, bool pAppend = false, bool pPeripherique = false, Encoding pEncode = null)
        {
            if (pEncode != null && !pPeripherique)
            {
                StreamWriter streamWriter = new StreamWriter(pNomFichier, pAppend, pEncode);
                streamWriter.Write(pTexte);
                streamWriter.Close();
            }
            else if (!pPeripherique)
            {
                int num = FileSystem.FreeFile();
                if (!pAppend)
                {
                    FileSystem.FileOpen(num, pNomFichier, OpenMode.Output, OpenAccess.Default, OpenShare.Default, -1);
                }
                else
                {
                    FileSystem.FileOpen(num, pNomFichier, OpenMode.Append, OpenAccess.Default, OpenShare.Default, -1);
                }
                FileSystem.Print(num, new object[] { pTexte });
                FileSystem.FileClose(new int[] { num });
            }
            else if (!RawPrinterHelper.IsPrinter(pNomFichier))
            {
                DirectPrint directPrint = new DirectPrint();
                directPrint.StartWrite(pNomFichier);
                directPrint.Write(pTexte);
                directPrint.EndWrite();
            }
            else
            {
                RawPrinterHelper.SendStringToPrinter(pNomFichier, pTexte);
            }
        }

        public static string FixUnicodeString(string pStr)
        {
            string str = "";
            for (int i = 0; i < pStr.Length && Strings.Asc(pStr[i]) != 0; i = checked(i + 1))
            {
                str = string.Concat(str, Conversions.ToString(pStr[i]));
            }
            return str;
        }

        public static string GetCPUInfos(bool pFullInfos = false)
        {
            RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("HARDWARE");
            RegistryKey registryKey1 = registryKey.OpenSubKey("DESCRIPTION").OpenSubKey("SYSTEM");
            RegistryKey registryKey2 = registryKey1.OpenSubKey("CentralProcessor").OpenSubKey("0");
            if (!pFullInfos)
            {
                return Conversions.ToString(registryKey2.GetValue("ProcessorNameString"));
            }
            return Conversions.ToString(Operators.ConcatenateObject(Operators.AddObject(Operators.AddObject(registryKey2.GetValue("ProcessorNameString"), " "), registryKey2.GetValue("~Mhz").ToString()), Operators.AddObject(Operators.AddObject(Operators.AddObject("MHz ", registryKey2.GetValue("VendorIdentifier")), " "), registryKey2.GetValue("Identifier").ToString())));
        }

        //public static DateTime GetDate(object s, [DateTimeConstant(599265216000000000L)] DateTime pDefaultDate = default(DateTime))
        //{
        //    if (Information.IsDBNull(RuntimeHelpers.GetObjectValue(s)))
        //    {
        //        return pDefaultDate;
        //    }
        //    return Conversions.ToDate(s);
        //}

        public static Encoding GetFileEncoding(string pNomFichier, Encoding pDefaultEncoding = null)
        {
            Encoding uTF8 = null;
            FileStream fileStream = new FileStream(pNomFichier, FileMode.Open, FileAccess.Read, FileShare.Read);
            if (!fileStream.CanSeek)
            {
                uTF8 = (pDefaultEncoding != null ? pDefaultEncoding : Encoding.Default);
            }
            else
            {
                byte[] numArray = new byte[5];
                fileStream.Read(numArray, 0, 4);
                if (numArray[0] == 239 & numArray[1] == 187 & numArray[2] == 191)
                {
                    uTF8 = Encoding.UTF8;
                }
                else if (numArray[0] == 255 & numArray[1] == 254 | numArray[0] == 0 & numArray[1] == 0 & numArray[2] == 254 & numArray[3] == 255)
                {
                    uTF8 = Encoding.Unicode;
                }
                else if (!(numArray[0] == 254 & numArray[1] == 255))
                {
                    uTF8 = (pDefaultEncoding != null ? pDefaultEncoding : Encoding.Default);
                }
                else
                {
                    uTF8 = Encoding.BigEndianUnicode;
                }
                fileStream.Seek((long)0, SeekOrigin.Begin);
            }
            fileStream.Close();
            return uTF8;
        }

        //public static string GetGeoCode(string pAdresse, bool UseGoogle = true, bool UseYahoo = true)
        //{
        //    XmlDocument geoCodeXML;
        //    string str = "";
        //    Monitor.Enter(Objets.$STATIC$GetGeoCode$03EE22$GoogleOverQuery$Init);
        //    try
        //    {
        //        if (Objets.$STATIC$GetGeoCode$03EE22$GoogleOverQuery$Init.State == 0)
        //        {
        //            Objets.$STATIC$GetGeoCode$03EE22$GoogleOverQuery$Init.State = 2;
        //            Objets.$STATIC$GetGeoCode$03EE22$GoogleOverQuery = false;
        //        }
        //        else if (Objets.$STATIC$GetGeoCode$03EE22$GoogleOverQuery$Init.State == 2)
        //        {
        //            throw new IncompleteInitialization();
        //        }
        //    }
        //    finally
        //    {
        //        Objets.$STATIC$GetGeoCode$03EE22$GoogleOverQuery$Init.State = 1;
        //        Monitor.Exit(Objets.$STATIC$GetGeoCode$03EE22$GoogleOverQuery$Init);
        //    }
        //    if (Objets.$STATIC$GetGeoCode$03EE22$GoogleOverQuery && DateTime.Compare(DateTime.Now, Objets.$STATIC$GetGeoCode$03EE22$GoogleEndOfOverQuery) > 0)
        //    {
        //        Objets.$STATIC$GetGeoCode$03EE22$GoogleOverQuery = false;
        //    }
        //    if (UseGoogle && !Objets.$STATIC$GetGeoCode$03EE22$GoogleOverQuery)
        //    {
        //        geoCodeXML = GoogleMap.GetGeoCodeXML(pAdresse);
        //        if (GoogleMap.XMLGetStatus(geoCodeXML) != GoogleMap.enXMLStatus.OK)
        //        {
        //            if (GoogleMap.XMLGetStatus(geoCodeXML) == GoogleMap.enXMLStatus.OVER_QUERY_LIMIT)
        //            {
        //                Objets.$STATIC$GetGeoCode$03EE22$GoogleOverQuery = true;
        //                DateTime dateTime = DateTime.Now.AddHours(24);
        //                Objets.$STATIC$GetGeoCode$03EE22$GoogleEndOfOverQuery = dateTime.AddMinutes(1);
        //            }
        //            if (UseYahoo)
        //            {
        //                geoCodeXML = YahooMap.GetGeoCodeXML(pAdresse);
        //                if (YahooMap.XMLGetStatus(geoCodeXML) == YahooMap.enXMLStatus.OK)
        //                {
        //                    str = string.Concat(YahooMap.XMLGetLatitude(geoCodeXML), ", ", YahooMap.XMLGetLongitude(geoCodeXML));
        //                }
        //            }
        //        }
        //        else
        //        {
        //            str = string.Concat(GoogleMap.XMLGetLatitude(geoCodeXML), ", ", GoogleMap.XMLGetLongitude(geoCodeXML));
        //        }
        //    }
        //    else if (UseYahoo)
        //    {
        //        geoCodeXML = YahooMap.GetGeoCodeXML(pAdresse);
        //        if (YahooMap.XMLGetStatus(geoCodeXML) == YahooMap.enXMLStatus.OK)
        //        {
        //            str = string.Concat(YahooMap.XMLGetLatitude(geoCodeXML), ", ", YahooMap.XMLGetLongitude(geoCodeXML));
        //        }
        //    }
        //    return str;
        //}

        //public static string GetHardwareKey()
        //{
        //    ManagementObjectCollection.ManagementObjectEnumerator enumerator = null;
        //    string str = "NID";
        //    using (ManagementObjectCollection instances = (new ManagementClass("Win32_Processor")).GetInstances())
        //    {
        //        enumerator = instances.GetEnumerator();
        //        while (enumerator.MoveNext())
        //        {
        //            ManagementObject current = (ManagementObject)enumerator.Current;
        //            str = string.Concat(str, current.Properties["ProcessorId"].Value.ToString());
        //        }
        //    }
        //    string str1 = string.Concat(MyProject.Computer.Name, str);
        //    return Objets.MD5(str1, true);
        //}

        public static Collection GetNetworkComputersList()
        {
            Collection collections = new Collection();
            ProcessStartInfo processStartInfo = new ProcessStartInfo()
            {
                FileName = "net.exe",
                WindowStyle = ProcessWindowStyle.Hidden,
                Arguments = "view",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                StandardOutputEncoding = Encoding.Default,
                CreateNoWindow = true
            };
            Process process = Process.Start(processStartInfo);
            Encoding currentEncoding = process.StandardOutput.CurrentEncoding;
            string end = (new StreamReader(process.StandardOutput.BaseStream)).ReadToEnd();
            process.WaitForExit();
            string[] strArrays = end.Split(new char[] { '\r' });
            for (int i = 0; i < checked((int)strArrays.Length); i = checked(i + 1))
            {
                string str = strArrays[i];
                if (str.Contains("\\\\"))
                {
                    collections.Add(str.Replace("\\\\", "").Trim(), null, null, null);
                }
            }
            return collections;
        }

        public static string GetProcessInfos()
        {
            string str = "ProcessName;ProcessId;RAM;VRAM\r\n";
            Process[] processes = Process.GetProcesses();
            for (int i = 0; i < checked((int)processes.Length); i = checked(i + 1))
            {
                Process process = processes[i];
                string[] processName = new string[] { str, process.ProcessName, ";", null, null, null, null, null, null };
                processName[3] = process.Id.ToString();
                processName[4] = ";";
                double workingSet64 = (double)process.WorkingSet64 / 1024;
                processName[5] = workingSet64.ToString();
                processName[6] = "K;";
                double pagedMemorySize64 = (double)process.PagedMemorySize64 / 1024;
                processName[7] = pagedMemorySize64.ToString();
                processName[8] = "K\r\n";
                str = string.Concat(processName);
            }
            return str;
        }

        public static string GetStr(object s, bool pSayNull = false)
        {
            if (!Information.IsDBNull(RuntimeHelpers.GetObjectValue(s)) && s != null)
            {
                return s.ToString();
            }
            if (pSayNull)
            {
                return "NULL";
            }
            return "";
        }

        //public static string GetSystemInfos()
        //{
        //    string[] str;
        //    double totalSize;
        //    double availableFreeSpace;
        //    string str1 = "";
        //    DateTime dateTime = new DateTime(2099, 12, 31);
        //    str1 = string.Concat(str1, "HostName : ", MyProject.Computer.Name, "\r\n");
        //    str1 = string.Concat(str1, "IP Address(es) : ");
        //    IPAddress[] hostAddresses = Dns.GetHostAddresses(Dns.GetHostName());
        //    for (int i = 0; i < checked((int)hostAddresses.Length); i = checked(i + 1))
        //    {
        //        IPAddress pAddress = hostAddresses[i];
        //        if (pAddress.AddressFamily == AddressFamily.InterNetwork)
        //        {
        //            str1 = string.Concat(str1, pAddress.ToString(), ", ");
        //        }
        //    }
        //    if (str1.EndsWith(", "))
        //    {
        //        str1 = str1.Substring(0, checked(str1.Length - 2));
        //    }
        //    str1 = string.Concat(str1, "\r\n");
        //    int count = checked(MyProject.Computer.FileSystem.Drives.Count - 1);
        //    for (int j = 0; j <= count; j = checked(j + 1))
        //    {
        //        try
        //        {
        //            str1 = string.Concat(str1, "Drive ", j.ToString(), " : ");
        //            DriveInfo item = MyProject.Computer.FileSystem.Drives[j];
        //            str = new string[] { str1, " [", item.DriveType.ToString(), "] ", item.Name };
        //            str1 = string.Concat(str);
        //            if (!item.IsReady)
        //            {
        //                str1 = string.Concat(str1, " -Not ready-");
        //            }
        //            else
        //            {
        //                str = new string[] { str1, " (", item.VolumeLabel, ") : ", null, null, null, null, null, null };
        //                availableFreeSpace = (double)item.AvailableFreeSpace / 1024 / 1024 / 1024;
        //                str[4] = availableFreeSpace.ToString("0.00");
        //                str[5] = "GB Free / ";
        //                totalSize = (double)item.TotalSize / 1024 / 1024 / 1024;
        //                str[6] = totalSize.ToString("0.00");
        //                str[7] = " GB (";
        //                str[8] = item.DriveFormat;
        //                str[9] = ")";
        //                str1 = string.Concat(str);
        //            }
        //            item = null;
        //            str1 = string.Concat(str1, "\r\n");
        //        }
        //        catch (Exception exception)
        //        {
        //            ProjectData.SetProjectError(exception);
        //            str1 = string.Concat(str1, exception.Message, "\r\n");
        //            ProjectData.ClearProjectError();
        //        }
        //    }
        //    str = new string[] { str1, "RAM : ", null, null, null, null };
        //    totalSize = (double)((float)MyProject.Computer.Info.AvailablePhysicalMemory) / 1024 / 1024;
        //    str[2] = totalSize.ToString("0.00");
        //    str[3] = " MB Free / ";
        //    availableFreeSpace = (double)((float)MyProject.Computer.Info.TotalPhysicalMemory) / 1024 / 1024;
        //    str[4] = availableFreeSpace.ToString("0.00");
        //    str[5] = " MB\r\n";
        //    str1 = string.Concat(str);
        //    str = new string[] { str1, "VRAM : ", null, null, null, null };
        //    totalSize = (double)((float)MyProject.Computer.Info.AvailableVirtualMemory) / 1024 / 1024;
        //    str[2] = totalSize.ToString("0.00");
        //    str[3] = " MB Free / ";
        //    availableFreeSpace = (double)((float)MyProject.Computer.Info.TotalVirtualMemory) / 1024 / 1024;
        //    str[4] = availableFreeSpace.ToString("0.00");
        //    str[5] = " MB\r\n";
        //    str1 = string.Concat(str);
        //    str1 = string.Concat(str1, "CPU : ", Objets.GetCPUInfos(false), "\r\n");
        //    str = new string[] { str1, "OS : ", MyProject.Computer.Info.OSFullName, " (", MyProject.Computer.Info.OSPlatform, ") ", MyProject.Computer.Info.OSVersion, "\r\n" };
        //    str1 = string.Concat(str);
        //    str1 = string.Concat(str1, "Culture : ", MyProject.Computer.Info.InstalledUICulture.DisplayName, "\r\n");
        //    str1 = string.Concat(str1, "GmtTime : ", Conversions.ToString(MyProject.Computer.Clock.GmtTime), "\r\n");
        //    str1 = string.Concat(str1, "LocalTime : ", Conversions.ToString(MyProject.Computer.Clock.LocalTime), "\r\n");
        //    str1 = string.Concat(str1, "UserName : ", MyProject.User.Name, "\r\n");
        //    str1 = string.Concat(str1, "Date format : ", dateTime.ToString(), "\r\n");
        //    str1 = string.Concat(str1, "DecimalSeparator : \"", MyProject.Application.Culture.NumberFormat.CurrencyDecimalSeparator.ToString(), "\"\r\n");
        //    str1 = string.Concat(str1, "GroupSeparator : \"", MyProject.Application.Culture.NumberFormat.CurrencyGroupSeparator.ToString(), "\"\r\n");
        //    int currencyDecimalDigits = MyProject.Application.Culture.NumberFormat.CurrencyDecimalDigits;
        //    str1 = string.Concat(str1, "DecimalDigits : ", currencyDecimalDigits.ToString(), "\r\n");
        //    int length = checked(checked((int)Screen.AllScreens.Length) - 1);
        //    for (int k = 0; k <= length; k = checked(k + 1))
        //    {
        //        Screen allScreens = Screen.AllScreens[k];
        //        str = new string[] { str1, "Display ", null, null, null, null, null, null, null, null, null, null };
        //        str[2] = (checked(k + 1)).ToString();
        //        str[3] = " : ";
        //        str[4] = Objets.FixUnicodeString(allScreens.DeviceName);
        //        str[5] = " ";
        //        str[6] = allScreens.Bounds.Width.ToString();
        //        str[7] = "x";
        //        str[8] = allScreens.Bounds.Height.ToString();
        //        str[9] = " ";
        //        currencyDecimalDigits = allScreens.BitsPerPixel;
        //        str[10] = currencyDecimalDigits.ToString();
        //        str[11] = "bits";
        //        str1 = string.Concat(str);
        //        str1 = Conversions.ToString(Operators.AddObject(str1, Interaction.IIf(allScreens.Primary, " (Primary display)", "")));
        //        str1 = string.Concat(str1, "\r\n");
        //        allScreens = null;
        //    }
        //    str1 = string.Concat(str1, "Program Files Directory : ", MyProject.Computer.FileSystem.SpecialDirectories.ProgramFiles, "\r\n");
        //    str1 = string.Concat(str1, "Temp Directory : ", MyProject.Computer.FileSystem.SpecialDirectories.Temp, "\r\n");
        //    str1 = string.Concat(str1, "Current Directory : ", MyProject.Computer.FileSystem.CurrentDirectory, "\r\n");
        //    return str1;
        //}

        public static double GetVal(object v, double pDefaultVal = 0)
        {
            if (Information.IsDBNull(RuntimeHelpers.GetObjectValue(v)))
            {
                return pDefaultVal;
            }
            return Conversions.ToDouble(v);
        }

        public static void GlobalDropSetting(string pAppName, string pSection = "", string pClee = "", bool pFailOnError = false, string pRemoteMachine = null)
        {
            RegistryKey localMachine = Registry.LocalMachine;
            if (pRemoteMachine != null)
            {
                localMachine = RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, pRemoteMachine);
            }
            if (Operators.CompareString(pClee, "", false) != 0)
            {
                localMachine.OpenSubKey(string.Concat("Software\\MG2S\\", pAppName, "\\", pSection), true).DeleteValue(pClee, pFailOnError);
            }
            else if (Operators.CompareString(pSection, "", false) != 0)
            {
                localMachine.DeleteSubKeyTree(string.Concat("Software\\MG2S\\", pAppName, "\\", pSection));
            }
            else
            {
                localMachine.DeleteSubKeyTree(string.Concat("Software\\MG2S\\", pAppName));
            }
        }

        public static object GlobalGetSetting(string pAppName, string pSection, string pClee, object pValDefaut = null, string pRemoteMachine = null)
        {
            RegistryKey localMachine = Registry.LocalMachine;
            if (pRemoteMachine != null)
            {
                localMachine = RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, pRemoteMachine);
            }
            if (localMachine.OpenSubKey("Software").OpenSubKey("MG2S") == null)
            {
                if (pValDefaut == null)
                {
                    return "";
                }
                return pValDefaut;
            }
            if (localMachine.OpenSubKey("Software").OpenSubKey("MG2S").OpenSubKey(pAppName) == null)
            {
                return pValDefaut;
            }
            if (localMachine.OpenSubKey("Software").OpenSubKey("MG2S").OpenSubKey(pAppName).OpenSubKey(pSection) == null)
            {
                return pValDefaut;
            }
            return localMachine.OpenSubKey("Software").OpenSubKey("MG2S").OpenSubKey(pAppName).OpenSubKey(pSection).GetValue(pClee, RuntimeHelpers.GetObjectValue(pValDefaut));
        }
        public static object GlobalGetSettingtest(string pAppName, string pSection, string pClee, object pValDefaut = null, string pRemoteMachine = null)
        {
            RegistryKey localMachine = Registry.CurrentUser;
            if (pRemoteMachine != null)
            {
                localMachine = RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, pRemoteMachine);
            }
            if (localMachine.OpenSubKey("Software").OpenSubKey("MG2S") == null)
            {
                if (pValDefaut == null)
                {
                    return "";
                }
                return pValDefaut;
            }
            if (localMachine.OpenSubKey("Software").OpenSubKey("MG2S").OpenSubKey(pAppName) == null)
            {
                return pValDefaut;
            }
            if (localMachine.OpenSubKey("Software").OpenSubKey("MG2S").OpenSubKey(pAppName).OpenSubKey(pSection) == null)
            {
                return pValDefaut;
            }
            return localMachine.OpenSubKey("Software").OpenSubKey("MG2S").OpenSubKey(pAppName).OpenSubKey(pSection).GetValue(pClee, RuntimeHelpers.GetObjectValue(pValDefaut));
        }

        public static string[] GlobalGetSettingsList(string pAppName, string pSection = "", string pRemoteMachine = null)
        {
            string[] valueNames;
            try
            {
                RegistryKey localMachine = Registry.LocalMachine;
                if (pRemoteMachine != null)
                {
                    localMachine = RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, pRemoteMachine);
                }
                valueNames = localMachine.OpenSubKey("Software").OpenSubKey("MG2S").OpenSubKey(pAppName).OpenSubKey(pSection).GetValueNames();
            }
            catch (Exception exception)
            {
                ProjectData.SetProjectError(exception);
                valueNames = null;
                ProjectData.ClearProjectError();
            }
            return valueNames;
        }

        public static void GlobalSaveSetting(string pAppName, string pSection, string pClee, object pVal, string pRemoteMachine = null)
        {
            RegistryKey localMachine = Registry.LocalMachine;
            if (pRemoteMachine != null)
            {
                localMachine = RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, pRemoteMachine);
            }
            localMachine = localMachine.CreateSubKey(string.Concat("Software\\MG2S\\", pAppName, "\\", pSection));
            localMachine.SetValue(pClee, RuntimeHelpers.GetObjectValue(pVal));
        }

        //public static string InputBox(string pMessage, string pTitre, string pDefaultValue = "", bool pPassword = false)
        //{
        //    frmInputBox _frmInputBox = new frmInputBox()
        //    {
        //        Text = pTitre
        //    };
        //    _frmInputBox.lMessage.Text = pMessage;
        //    if (pPassword)
        //    {
        //        _frmInputBox.tReponse.PasswordChar = '*';
        //    }
        //    _frmInputBox.tReponse.Text = pDefaultValue;
        //    if (_frmInputBox.ShowDialog() != DialogResult.OK)
        //    {
        //        return "";
        //    }
        //    return _frmInputBox.tReponse.Text;
        //}

        [DllImport("wininet.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        private static extern long InternetGetConnectedState(ref long lpdwFlags, long dwReserved);

        public static bool IsPathValid(string pPath)
        {
            bool flag;
            try
            {
                flag = (!Path.IsPathRooted(pPath) ? true : true);
            }
            catch (Exception exception)
            {
                ProjectData.SetProjectError(exception);
                flag = false;
                ProjectData.ClearProjectError();
            }
            return flag;
        }

        public static bool IsStrEmailValid(string pStrEmail)
        {
            return Regex.IsMatch(pStrEmail, "[-0-9a-zA-Z.+_]+@[-0-9a-zA-Z.+_]+\\.[a-zA-Z]{2,4}");
        }

        public static bool IsUpperVersion(string CurrentVersionStr, string ComparedVersionStr)
        {
            char[] chrArray = new char[] { '.' };
            string[] strArrays = CurrentVersionStr.Split(chrArray);
            chrArray = new char[] { '.' };
            string[] strArrays1 = ComparedVersionStr.Split(chrArray);
            int num = Math.Min(checked((int)strArrays.Length), checked((int)strArrays1.Length));
            int num1 = checked(num - 1);
            for (int i = 0; i <= num1; i = checked(i + 1))
            {
                int integer = 0;
                if (checked((int)strArrays.Length) > i && Operators.CompareString(Objets.StrOnlyNums(strArrays[i], false), "", false) != 0)
                {
                    integer = Conversions.ToInteger(Objets.StrOnlyNums(strArrays[i], false));
                }
                int integer1 = 0;
                if (checked((int)strArrays1.Length) > i && Operators.CompareString(Objets.StrOnlyNums(strArrays1[i], false), "", false) != 0)
                {
                    integer1 = Conversions.ToInteger(Objets.StrOnlyNums(strArrays1[i], false));
                }
                if (integer1 > integer)
                {
                    return true;
                }
                if (integer1 < integer)
                {
                    return false;
                }
            }
            return false;
        }

        //public static bool IsWANConnected(string ReturnConnType = "")
        //{
        //    long num = 0L;
        //    bool flag;
        //    try
        //    {
        //        bool flag1 = Objets.InternetGetConnectedState(ref num, (long)0) != (long)0;
        //        if ((num & (long)2) != (long)0)
        //        {
        //            ReturnConnType = string.Concat(ReturnConnType, "LAN ");
        //        }
        //        if ((num & (long)1) != (long)0)
        //        {
        //            ReturnConnType = string.Concat(ReturnConnType, "Modem ");
        //        }
        //        if ((num & (long)4) != (long)0)
        //        {
        //            ReturnConnType = string.Concat(ReturnConnType, "Proxy ");
        //        }
        //        if ((num & (long)32) != (long)0)
        //        {
        //            ReturnConnType = string.Concat(ReturnConnType, "Offline ");
        //        }
        //        if ((num & (long)64) != (long)0)
        //        {
        //            ReturnConnType = string.Concat(ReturnConnType, "Configured ");
        //        }
        //        if ((num & (long)16) != (long)0)
        //        {
        //            ReturnConnType = string.Concat(ReturnConnType, "RemoteAccessServer");
        //        }
        //        flag = (!flag1 ? false : MyProject.Computer.Network.Ping("8.8.8.8"));
        //    }
        //    catch (Exception exception)
        //    {
        //        ProjectData.SetProjectError(exception);
        //        flag = false;
        //        ProjectData.ClearProjectError();
        //    }
        //    return flag;
        //}

        public static void LocalDropSetting(string pAppName, string pSection = "", string pClee = "", bool pFailOnError = false)
        {
            if (Operators.CompareString(pClee, "", false) != 0)
            {
                Registry.CurrentUser.OpenSubKey(string.Concat("Software\\MG2S\\", pAppName, "\\", pSection), true).DeleteValue(pClee, pFailOnError);
            }
            else if (Operators.CompareString(pSection, "", false) != 0)
            {
                Registry.CurrentUser.DeleteSubKeyTree(string.Concat("Software\\MG2S\\", pAppName, "\\", pSection));
            }
            else
            {
                Registry.CurrentUser.DeleteSubKeyTree(string.Concat("Software\\MG2S\\", pAppName));
            }
        }

        public static object LocalGetSetting(string pAppName, string pSection, string pClee, object pValDefaut = null)
        {
            if (Registry.CurrentUser.OpenSubKey("Software").OpenSubKey("MG2S") == null)
            {
                if (pValDefaut == null)
                {
                    return "";
                }
                return pValDefaut;
            }
            if (Registry.CurrentUser.OpenSubKey("Software").OpenSubKey("MG2S").OpenSubKey(pAppName) == null)
            {
                return pValDefaut;
            }
            if (Registry.CurrentUser.OpenSubKey("Software").OpenSubKey("MG2S").OpenSubKey(pAppName).OpenSubKey(pSection) == null)
            {
                return pValDefaut;
            }
            return Registry.CurrentUser.OpenSubKey("Software").OpenSubKey("MG2S").OpenSubKey(pAppName).OpenSubKey(pSection).GetValue(pClee, RuntimeHelpers.GetObjectValue(pValDefaut));
        }

        public static string[] LocalGetSettingsList(string pAppName, string pSection = "")
        {
            return Registry.CurrentUser.OpenSubKey("Software").OpenSubKey("MG2S").OpenSubKey(pAppName).OpenSubKey(pSection).GetValueNames();
        }

        public static void LocalSaveSetting(string pAppName, string pSection, string pClee, object pVal)
        {
            
            Registry.SetValue(string.Concat("HKEY_CURRENT_USER\\Software\\MG2S\\", pAppName, "\\", pSection), pClee, RuntimeHelpers.GetObjectValue(pVal));
           // Registry.SetValue(string.Concat("HKEY_LOCAL_MACHINE\\Software\\MG2S\\", pAppName, "\\", pSection), pClee, RuntimeHelpers.GetObjectValue(pVal));
        }

        public static void LVLoadColsSize(Form pForm, ListView pLV, string pAppName = "", bool pGlobal = false)
        {
            int i;
            string productName = pAppName;
            if (Operators.CompareString(productName, "", false) == 0)
            {
                productName = Application.ProductName;
            }
            string name = pForm.Name;
            string str = pLV.Name;
            string str1 = string.Concat("Form", name, "LV", str);
            if (!pGlobal)
            {
                int count = checked(pLV.Columns.Count - 1);
                for (i = 0; i <= count; i = checked(i + 1))
                {
                    pLV.Columns[i].Width = Conversions.ToInteger(Objets.LocalGetSetting(productName, str1, string.Concat("col", i.ToString()), pLV.Columns[i].Width));
                }
            }
            else
            {
                int num = checked(pLV.Columns.Count - 1);
                for (i = 0; i <= num; i = checked(i + 1))
                {
                    pLV.Columns[i].Width = Conversions.ToInteger(Objets.GlobalGetSetting(productName, str1, string.Concat("col", i.ToString()), pLV.Columns[i].Width, null));
                }
            }
        }

        public static void LVSaveColsSize(Form pForm, ListView pLV, string pAppName = "", bool pGlobal = false)
        {
            int i;
            string productName = pAppName;
            if (Operators.CompareString(productName, "", false) == 0)
            {
                productName = Application.ProductName;
            }
            string name = pForm.Name;
            string str = pLV.Name;
            string str1 = string.Concat("Form", name, "LV", str);
            if (!pGlobal)
            {
                int count = checked(pLV.Columns.Count - 1);
                for (i = 0; i <= count; i = checked(i + 1))
                {
                    Objets.LocalSaveSetting(productName, str1, string.Concat("col", i.ToString()), pLV.Columns[i].Width);
                }
            }
            else
            {
                int num = checked(pLV.Columns.Count - 1);
                for (i = 0; i <= num; i = checked(i + 1))
                {
                    Objets.GlobalSaveSetting(productName, str1, string.Concat("col", i.ToString()), pLV.Columns[i].Width, null);
                }
            }
        }

        public static object Max(double a, double b)
        {
            if (a > b)
            {
                return a;
            }
            return b;
        }

        public static void MaximizeRam(bool pUseSwap = true)
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            if (pUseSwap)
            {
                Objets.SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public static string MD5(string pStr, bool pModeCompatible = false)
        {
            byte[] bytes;
            byte[] numArray;
            MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider();
            if (!pModeCompatible)
            {
                bytes = Encoding.UTF8.GetBytes(pStr);
                numArray = mD5CryptoServiceProvider.ComputeHash(bytes);
                mD5CryptoServiceProvider.Clear();
                return Convert.ToBase64String(numArray);
            }
            StringBuilder stringBuilder = new StringBuilder();
            bytes = Encoding.Default.GetBytes(pStr);
            numArray = mD5CryptoServiceProvider.ComputeHash(bytes);
            mD5CryptoServiceProvider.Clear();
            byte[] numArray1 = numArray;
            for (int i = 0; i < checked((int)numArray1.Length); i = checked(i + 1))
            {
                byte num = numArray1[i];
                if (num <= 15)
                {
                    stringBuilder.Append("0");
                    stringBuilder.Append(Conversion.Hex(num));
                }
                else
                {
                    stringBuilder.Append(Conversion.Hex(num));
                }
            }
            return stringBuilder.ToString().ToLower();
        }

        public static object Min(double a, double b)
        {
            if (a < b)
            {
                return a;
            }
            return b;
        }

        public static string RSAExportKeys(bool IncludePrivateParameters = false)
        {
            return Convert.ToBase64String(Objets.MyRSA.ExportCspBlob(IncludePrivateParameters));
        }

        public static string RSAExportKeysToXML(bool IncludePrivateParameters = false)
        {
            return Objets.MyRSA.ToXmlString(IncludePrivateParameters);
        }

        public static string RSAGetSignature(string pData)
        {
            return Convert.ToBase64String(Objets.MyRSA.SignData(Encoding.UTF8.GetBytes(pData), new SHA1CryptoServiceProvider()));
        }

        public static void RSALoadKeys(string pKey)
        {
            Objets.MyRSA.ImportCspBlob(Convert.FromBase64CharArray(Conversions.ToCharArrayRankOne(pKey), 0, pKey.Length));
        }

        public static void RSALoadKeysFromXML(string pKey)
        {
            Objets.MyRSA.FromXmlString(pKey);
        }

        public static string RSAStrCrypt(string pStr)
        {
            return Convert.ToBase64String(Objets.MyRSA.Encrypt(Encoding.UTF8.GetBytes(pStr), true));
        }

        public static string RSAStrDecrypt(string pStr)
        {
            return Encoding.UTF8.GetString(Objets.MyRSA.Decrypt(Convert.FromBase64CharArray(Conversions.ToCharArrayRankOne(pStr), 0, pStr.Length), true));
        }

        public static bool RSAVerifySignature(string pData, string pSignature)
        {
            return Objets.MyRSA.VerifyData(Encoding.UTF8.GetBytes(pData), new SHA1CryptoServiceProvider(), Convert.FromBase64CharArray(Conversions.ToCharArrayRankOne(pSignature), 0, pSignature.Length));
        }

        //public static void SendKeys(string pKeys)
        //{
        //    SendKeys.SendWait(pKeys);
        //}

        [DllImport("kernel32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        public static extern int SetProcessWorkingSetSize(IntPtr process, int minimumWorkingSetSize, int maximumWorkingSetSize);

        public static string SHA1(string pStr, bool pModeCompacte = false)
        {
            byte[] bytes;
            byte[] numArray;
            SHA1CryptoServiceProvider sHA1CryptoServiceProvider = new SHA1CryptoServiceProvider();
            if (pModeCompacte)
            {
                bytes = Encoding.UTF8.GetBytes(pStr);
                numArray = sHA1CryptoServiceProvider.ComputeHash(bytes);
                sHA1CryptoServiceProvider.Clear();
                return Convert.ToBase64String(numArray);
            }
            StringBuilder stringBuilder = new StringBuilder();
            bytes = Encoding.Default.GetBytes(pStr);
            numArray = sHA1CryptoServiceProvider.ComputeHash(bytes);
            sHA1CryptoServiceProvider.Clear();
            byte[] numArray1 = numArray;
            for (int i = 0; i < checked((int)numArray1.Length); i = checked(i + 1))
            {
                byte num = numArray1[i];
                if (num <= 15)
                {
                    stringBuilder.Append("0");
                    stringBuilder.Append(Conversion.Hex(num));
                }
                else
                {
                    stringBuilder.Append(Conversion.Hex(num));
                }
            }
            return stringBuilder.ToString().ToLower();
        }

        public static object StrCentre(string pStr, int NbCar)
        {
            string str = Strings.Left(pStr, NbCar);
            string str1 = Strings.Space(checked((int)Math.Round((double)(checked(NbCar - str.Length)) / 2)));
            str = string.Concat(str1, str, str1);
            return Objets.StrFix(str, NbCar, false);
        }

        public static string StrClean(string pStr)
        {
            if (pStr == null)
            {
                return "";
            }
            if (!Objets.StrCleanModeMySql)
            {
                return Strings.Trim(Regex.Replace(pStr, "'", "''").Replace("’", "''"));
            }
            return Strings.Trim(pStr.Replace("'", "''").Replace("\\", "\\\\").Replace("’", "''"));
        }

        public static string StrCleanKeepSpaces(string pStr)
        {
            return Regex.Replace(pStr, "'", "''");
        }

        public static string StrCleanXML(string pStr)
        {
            if (pStr == null)
            {
                return "";
            }
            return SecurityElement.Escape(pStr);
        }

        public static string StrCleanXMLDIAPI(string pStr)
        {
            if (pStr == null)
            {
                return "";
            }
            return SecurityElement.Escape(pStr).Replace("'", "&apos;").Replace("\"", "&quot;");
        }

        public static string StrCrypt(string pStr, string pKey)
        {
            string str = Objets.MD5(string.Concat("*MG2S!", pKey, "!MG2S*"), false);
            byte[] numArray = new byte[] { 8, 132, 5, 44, 76, 98, 7, 9 };
            byte[] numArray1 = numArray;
            Encoding encoding = Encoding.GetEncoding("ISO-8859-1");
            byte[] bytes = encoding.GetBytes(str);
            byte[] bytes1 = encoding.GetBytes(pStr);
            ICryptoTransform cryptoTransform = (new TripleDESCryptoServiceProvider()).CreateEncryptor(bytes, numArray1);
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoTransform, CryptoStreamMode.Write);
            cryptoStream.Write(bytes1, 0, checked((int)bytes1.Length));
            cryptoStream.FlushFinalBlock();
            memoryStream.Position = (long)0;
            byte[] numArray2 = new byte[checked(checked((int)memoryStream.Length) + 1)];
            memoryStream.Read(numArray2, 0, checked((int)memoryStream.Length));
            cryptoStream.Close();
            return Convert.ToBase64String(numArray2);
        }

        public static string StrDecrypt(string pStr, string pKey, bool pNoFail = false)
        {
            string str;
            string str1 = Objets.MD5(string.Concat("*MG2S!", pKey, "!MG2S*"), false);
            byte[] numArray = Convert.FromBase64String(pStr);
            byte[] numArray1 = new byte[] { 8, 132, 5, 44, 76, 98, 7, 9 };
            byte[] numArray2 = numArray1;
            Encoding encoding = Encoding.GetEncoding("ISO-8859-1");
            if (Operators.CompareString(pStr, "", false) == 0)
            {
                return "";
            }
            try
            {
                byte[] bytes = encoding.GetBytes(str1);
                ICryptoTransform cryptoTransform = (new TripleDESCryptoServiceProvider()).CreateDecryptor(bytes, numArray2);
                MemoryStream memoryStream = new MemoryStream();
                CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoTransform, CryptoStreamMode.Write);
                cryptoStream.Write(numArray, 0, checked(checked((int)numArray.Length) - 1));
                cryptoStream.FlushFinalBlock();
                memoryStream.Position = (long)0;
                byte[] numArray3 = new byte[checked(checked((int)(checked(memoryStream.Length - (long)1))) + 1)];
                memoryStream.Read(numArray3, 0, checked((int)memoryStream.Length));
                cryptoStream.Close();
                str = encoding.GetString(numArray3, 0, checked((int)numArray3.Length));
            }
            catch (Exception exception)
            {
                ProjectData.SetProjectError(exception);
                if (!pNoFail)
                {
                    throw;
                }
                else
                {
                    str = "";
                    ProjectData.ClearProjectError();
                }
            }
            return str;
        }

        public static string StrFix(string pStr, int NbCar, bool AligneADroite = false)
        {
            if (AligneADroite)
            {
                return Strings.Left(pStr, NbCar).PadLeft(NbCar);
            }
            return Strings.Left(pStr, NbCar).PadRight(NbCar);
        }

        public static string StrOnlyNums(string pStr, bool pGardePoint = false)
        {
            string str = "";
            int length = checked(pStr.Length - 1);
            for (int i = 0; i <= length; i = checked(i + 1))
            {
                if (char.IsDigit(pStr, i) || pGardePoint && Operators.CompareString(Conversions.ToString(pStr[i]), ".", false) == 0)
                {
                    str = string.Concat(str, Conversions.ToString(pStr[i]));
                }
            }
            return str;
        }

        public static string StrReplace(string pStr, string pRecherche, string pRemplace, bool pSensibleCasse = false)
        {
            if (pSensibleCasse || Operators.CompareString(pStr, "", false) == 0)
            {
                return pStr.Replace(pRecherche, pRemplace);
            }
            return Strings.Replace(pStr, pRecherche, pRemplace, 1, -1, CompareMethod.Text);
        }

        public static string StrSansAccents(string pStr)
        {
            byte[] bytes = Encoding.GetEncoding(1251).GetBytes(pStr);
            return Encoding.ASCII.GetString(bytes);
        }

        public static string[] StrSplitWithDblQuote(string pStr, string pSeparator)
        {
            ArrayList arrayLists = new ArrayList();
            int num = 0;
            int num1 = pStr.IndexOf(pSeparator, num);
            string str = pStr.Substring(num);
            int num2 = 0;
            string str1 = "";
            int length = 0;
            while (str.Length != 0)
            {
                length = 0;
                if (!str.StartsWith("\""))
                {
                    length = str.IndexOf(pSeparator);
                    if (length == -1)
                    {
                        arrayLists.Add(str.Substring(0, str.Length));
                        length = checked(str.Length + 2);
                    }
                    else
                    {
                        arrayLists.Add(str.Substring(0, length));
                        length = checked(length + 1);
                    }
                }
                else
                {
                    str1 = str;
                    num2 = str1.IndexOf("\"", 1);
                    while (num2 != -1)
                    {
                        if (str1.Length == checked(num2 + 1))
                        {
                            length = checked(str1.Length - 2);
                            arrayLists.Add(str.Substring(1, length));
                            length = checked(length + 4);
                            goto Label0;
                        }
                        else if (Operators.CompareString(Conversions.ToString(str1[checked(num2 + 1)]), "\"", false) != 0)
                        {
                            if (Operators.CompareString(Conversions.ToString(str1[checked(num2 + 1)]), pSeparator, false) != 0)
                            {
                                throw new Exception("Le spérateur n'est pas présent après la fin du champs");
                            }
                            if (length == 0)
                            {
                                length = checked(num2 - 2);
                            }
                            arrayLists.Add(str.Substring(1, checked(length + 1)));
                            length = checked(length + 4);
                            goto Label0;
                        }
                        else
                        {
                            length = checked(length + checked(num2 + 1));
                            str1 = str1.Substring(checked(length + 1));
                            num2 = str1.IndexOf("\"", 1);
                        }
                    }
                    if (num2 == -1)
                    {
                        throw new Exception("Pas de guillemet de sortie de champs");
                    }
                }
            Label0:
                if (length <= checked(str.Length - 1))
                {
                    str = str.Substring(length);
                    num1 = str.IndexOf(pSeparator);
                }
                else
                {
                    str = "";
                    num1 = -1;
                }
            }
            if (pStr.EndsWith(";"))
            {
                arrayLists.Add("");
            }
            return (string[])arrayLists.ToArray(typeof(string));
        }

        public static long TimeStamp(DateTime pDate)
        {
            DateTime dateTime = new DateTime(1970, 1, 1);
            return DateAndTime.DateDiff(DateInterval.Second, dateTime, pDate, FirstDayOfWeek.Sunday, FirstWeekOfYear.Jan1);
        }

        public static long TimeStamp()
        {
            return Objets.TimeStamp(DateTime.Now);
        }

        public static string ValClean(object pVal, bool pFiltreCars = false, bool pSayNull = true)
        {
            string str;
            str = (!pFiltreCars ? Regex.Replace(pVal.ToString(), ",", ".") : Objets.StrOnlyNums(Regex.Replace(pVal.ToString(), ",", "."), true));
            if (pSayNull && Operators.CompareString(str.Trim(), "", false) == 0)
            {
                return "NULL";
            }
            return str;
        }

        public static void WinFormLoadSizePos(Form pForm, string pAppName = "", bool pGlobal = false)
        {
            string productName = pAppName;
            if (Operators.CompareString(productName, "", false) == 0)
            {
                productName = Application.ProductName;
            }
            if (!pGlobal)
            {
                pForm.WindowState = (FormWindowState)Conversions.ToInteger(Objets.LocalGetSetting(productName, string.Concat("Form", pForm.Name), "WindowState", pForm.WindowState));
                if (pForm.WindowState == FormWindowState.Normal)
                {
                    pForm.Left = Conversions.ToInteger(Objets.LocalGetSetting(productName, string.Concat("Form", pForm.Name), "Left", pForm.Left));
                    pForm.Top = Conversions.ToInteger(Objets.LocalGetSetting(productName, string.Concat("Form", pForm.Name), "Top", pForm.Top));
                    pForm.Width = Conversions.ToInteger(Objets.LocalGetSetting(productName, string.Concat("Form", pForm.Name), "Width", pForm.Width));
                    pForm.Height = Conversions.ToInteger(Objets.LocalGetSetting(productName, string.Concat("Form", pForm.Name), "Height", pForm.Height));
                }
            }
            else
            {
                pForm.WindowState = (FormWindowState)Conversions.ToInteger(Objets.GlobalGetSetting(productName, string.Concat("Form", pForm.Name), "WindowState", pForm.WindowState, null));
                if (pForm.WindowState == FormWindowState.Normal)
                {
                    pForm.Left = Conversions.ToInteger(Objets.GlobalGetSetting(productName, string.Concat("Form", pForm.Name), "Left", pForm.Left, null));
                    pForm.Top = Conversions.ToInteger(Objets.GlobalGetSetting(productName, string.Concat("Form", pForm.Name), "Top", pForm.Top, null));
                    pForm.Width = Conversions.ToInteger(Objets.GlobalGetSetting(productName, string.Concat("Form", pForm.Name), "Width", pForm.Width, null));
                    pForm.Height = Conversions.ToInteger(Objets.GlobalGetSetting(productName, string.Concat("Form", pForm.Name), "Height", pForm.Height, null));
                }
            }
        }

        public static void WinFormSaveSizePos(Form pForm, string pAppName = "", bool pGlobal = false)
        {
            string productName = pAppName;
            if (Operators.CompareString(productName, "", false) == 0)
            {
                productName = Application.ProductName;
            }
            if (!pGlobal)
            {
                if (pForm.WindowState == FormWindowState.Normal)
                {
                    Objets.LocalSaveSetting(productName, string.Concat("Form", pForm.Name), "Left", pForm.Left);
                    Objets.LocalSaveSetting(productName, string.Concat("Form", pForm.Name), "Top", pForm.Top);
                    Objets.LocalSaveSetting(productName, string.Concat("Form", pForm.Name), "Width", pForm.Width);
                    Objets.LocalSaveSetting(productName, string.Concat("Form", pForm.Name), "Height", pForm.Height);
                }
                Objets.LocalSaveSetting(productName, string.Concat("Form", pForm.Name), "WindowState", (int)pForm.WindowState);
            }
            else
            {
                if (pForm.WindowState == FormWindowState.Normal)
                {
                    Objets.GlobalSaveSetting(productName, string.Concat("Form", pForm.Name), "Left", pForm.Left, null);
                    Objets.GlobalSaveSetting(productName, string.Concat("Form", pForm.Name), "Top", pForm.Top, null);
                    Objets.GlobalSaveSetting(productName, string.Concat("Form", pForm.Name), "Width", pForm.Width, null);
                    Objets.GlobalSaveSetting(productName, string.Concat("Form", pForm.Name), "Height", pForm.Height, null);
                }
                Objets.GlobalSaveSetting(productName, string.Concat("Form", pForm.Name), "WindowState", (int)pForm.WindowState, null);
            }
        }

        public static void WriteDataTableInFile(DataTable sourceTable, TextWriter writer, string pSeparator = ";", bool pIncludeFieldName = true)
        {
            IEnumerator enumerator = null;
            IEnumerator enumerator1 = null;
            int num = 0;
            string str = "";
            try
            {
                enumerator = sourceTable.Columns.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    DataColumn current = (DataColumn)enumerator.Current;
                    if (Operators.CompareString(str, "", false) != 0)
                    {
                        str = string.Concat(str, pSeparator);
                    }
                    str = string.Concat(str, current.ColumnName);
                    num = checked(num + 1);
                }
            }
            finally
            {
                if (enumerator is IDisposable)
                {
                    (enumerator as IDisposable).Dispose();
                }
            }
            if (pIncludeFieldName)
            {
                writer.WriteLine(str);
            }
            string str1 = "";
            try
            {
                enumerator1 = sourceTable.Rows.GetEnumerator();
                while (enumerator1.MoveNext())
                {
                    DataRow dataRow = (DataRow)enumerator1.Current;
                    int num1 = checked(num - 1);
                    for (int i = 0; i <= num1; i = checked(i + 1))
                    {
                        if (i != 0)
                        {
                            str1 = string.Concat(str1, pSeparator);
                        }
                        str1 = (sourceTable.Columns[i].DataType != typeof(string) ? string.Concat(str1, dataRow[i].ToString()) : string.Concat(str1, "\"", dataRow[i].ToString(), "\""));
                    }
                    writer.WriteLine(str1);
                    str1 = "";
                }
            }
            finally
            {
                if (enumerator1 is IDisposable)
                {
                    (enumerator1 as IDisposable).Dispose();
                }
            }
            writer.Flush();
        }

        public static string XMLCleanDIAPI(string pStr)
        {
            if (pStr == null)
            {
                return "";
            }
            return pStr.Replace("'", "&apos;");
        }

        public static double xVal(string pStr)
        {
            if (pStr == null)
            {
                return 0;
            }
            int num = Strings.InStr(pStr, ",", CompareMethod.Binary);
            int num1 = Strings.InStr(pStr, ".", CompareMethod.Binary);
            string str = pStr.Replace("'", "");
            str = pStr.Replace("€", "").Replace("%", "");
            if (num > 0 && num < num1)
            {
                str = str.Replace(",", "");
            }
            if (num1 > 0 && num1 < num)
            {
                str = str.Replace(".", "");
            }
            return Conversion.Val(str.Replace(",", "."));
        }

        public enum enExecMode
        {
            Service = 1,
            Manuel = 2,
            ExternalExec = 3
        }
    }
}