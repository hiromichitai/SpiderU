﻿//------------------------------------------------------------------------------
// <auto-generated>
//     このコードはツールによって生成されました。
//     ランタイム バージョン:4.0.30319.34209
//
//     このファイルへの変更は、以下の状況下で不正な動作の原因になったり、
//     コードが再生成されるときに損失したりします。
// </auto-generated>
//------------------------------------------------------------------------------

namespace SpiderU.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "12.0.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool CSVAddComment {
            get {
                return ((bool)(this["CSVAddComment"]));
            }
            set {
                this["CSVAddComment"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool CSVAddHeader {
            get {
                return ((bool)(this["CSVAddHeader"]));
            }
            set {
                this["CSVAddHeader"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool useAutoFileName {
            get {
                return ((bool)(this["useAutoFileName"]));
            }
            set {
                this["useAutoFileName"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string autoFileNamePrefix {
            get {
                return ((string)(this["autoFileNamePrefix"]));
            }
            set {
                this["autoFileNamePrefix"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string autoFileNameSuffix {
            get {
                return ((string)(this["autoFileNameSuffix"]));
            }
            set {
                this["autoFileNameSuffix"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("2")]
        public int autoFileNameSerialDigits {
            get {
                return ((int)(this["autoFileNameSerialDigits"]));
            }
            set {
                this["autoFileNameSerialDigits"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("1")]
        public int autoFileNameSerialNumber {
            get {
                return ((int)(this["autoFileNameSerialNumber"]));
            }
            set {
                this["autoFileNameSerialNumber"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public int outputFileFormatID {
            get {
                return ((int)(this["outputFileFormatID"]));
            }
            set {
                this["outputFileFormatID"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public int CSVEncodingID {
            get {
                return ((int)(this["CSVEncodingID"]));
            }
            set {
                this["CSVEncodingID"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool syncAllScope {
            get {
                return ((bool)(this["syncAllScope"]));
            }
            set {
                this["syncAllScope"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string defaultAutoFileNameSuffixFormat {
            get {
                return ((string)(this["defaultAutoFileNameSuffixFormat"]));
            }
            set {
                this["defaultAutoFileNameSuffixFormat"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool CSVCommentIncludeModelName {
            get {
                return ((bool)(this["CSVCommentIncludeModelName"]));
            }
            set {
                this["CSVCommentIncludeModelName"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool CSVCommentIncludeDateTime {
            get {
                return ((bool)(this["CSVCommentIncludeDateTime"]));
            }
            set {
                this["CSVCommentIncludeDateTime"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("10")]
        public int fontSize {
            get {
                return ((int)(this["fontSize"]));
            }
            set {
                this["fontSize"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("yyyyMMdd-")]
        public string autoFileNamePrefixInitialValue {
            get {
                return ((string)(this["autoFileNamePrefixInitialValue"]));
            }
            set {
                this["autoFileNamePrefixInitialValue"] = value;
            }
        }
    }
}
