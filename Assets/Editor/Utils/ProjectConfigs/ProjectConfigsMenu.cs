using UnityEngine;
using UnityEditor;
using System.IO;

namespace com.ich.Editor.Utils.ProjectConfigs
{
    /// <summary>
    /// Define methods to set up PlayerSettings
    /// </summary>
    static class ProjectConfigsMenu
    {
        #region consts
        private const string ROOT_PATH = "/Editor/Utils/ProjectConfigs/";
        private const string PROD_CONFIG = ROOT_PATH + "prod_config.json";
        private const string DEV_CONFIG = ROOT_PATH + "dev_config.json";
        #endregion


        #region MenuItems
        [MenuItem("Utils/Settings/Open src", false, 0)]
        static void DoOpenSrc()
        {
            Application.OpenURL(Application.dataPath + ROOT_PATH + "ProjectConfigs.cs");
        }

        [MenuItem("Utils/Settings/toProd")]
        static void SwitchTOProd()
        {
            ProjectSetup(PROD_CONFIG);
        }

        [MenuItem("Utils/Settings/toDev")]
        static void SwitchToDev()
        {
            ProjectSetup(DEV_CONFIG);
        }
        #endregion

        /// <summary>
        /// Set up PlayerSettings
        /// </summary>
        /// <param name="shortPath"></param>
        private static void ProjectSetup(string shortPath)
        {
            var path = Application.dataPath + shortPath;

            if (!File.Exists(path))
            {
                throw new System.Exception(string.Format("Config is not exists : {0}", path));
            }

            var configStr = File.ReadAllText(path);

            if (string.IsNullOrEmpty(configStr))
            {
                throw new System.Exception(string.Format("Config is empty or null : {0}", path));
            }

            var prj = JsonUtility.FromJson<ProjectConfig>(configStr);

            Debug.Log("project has sett up to :\n" + prj.ToString());
            PlayerSettings.companyName = prj.companyName;
            PlayerSettings.productName = prj.productName;

            //app id setup:
            PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.Android, prj.android_appID);
            PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.iOS, prj.ios_appID);

            //version number setup:
            PlayerSettings.bundleVersion = prj.bundleVersion;
            PlayerSettings.Android.bundleVersionCode = prj.bundleVersionCode;
            PlayerSettings.iOS.buildNumber = prj.buildNumber;

            //ios specific setup:
            PlayerSettings.iOS.applicationDisplayName = prj.applicationDisplayName;
            PlayerSettings.iOS.appleEnableAutomaticSigning = prj.ios_appleEnableAutomaticSigning;
            PlayerSettings.iOS.targetOSVersionString = prj.ios_targetOSVersionString;
            PlayerSettings.iOS.iOSManualProvisioningProfileID = prj.ios_manualProvisioningProfileID;
            PlayerSettings.iOS.appleDeveloperTeamID = prj.ios_appleDeveloperTeamID;

            //android specific setup:
            PlayerSettings.Android.keystoreName = Application.dataPath + prj.android_keystoreName;
            PlayerSettings.Android.keystorePass = prj.android_keystorePass;
            PlayerSettings.Android.keyaliasName = prj.android_keyaliasName;
            PlayerSettings.Android.keyaliasPass = prj.android_keyaliasPass;

            //custom setup (not from json)
            //PlayerSettings.iOS.backgroundModes = iOSBackgroundMode.RemoteNotification;
            PlayerSettings.defaultInterfaceOrientation = UIOrientation.Portrait;
            PlayerSettings.allowedAutorotateToLandscapeLeft = false;
            PlayerSettings.allowedAutorotateToLandscapeRight = false;
            PlayerSettings.allowedAutorotateToPortrait = true;
            PlayerSettings.allowedAutorotateToPortraitUpsideDown = false;
            PlayerSettings.SplashScreen.showUnityLogo = false;
            PlayerSettings.Android.preferredInstallLocation = AndroidPreferredInstallLocation.Auto;
        }

        /// <summary>
        /// model of _config.json
        /// </summary>
        private class ProjectConfig
        {
#pragma warning disable 0649
            public string companyName;
            public string productName;
            public string bundleVersion;
            public string buildNumber;
            public int bundleVersionCode;
            public string applicationDisplayName;

            public string ios_appID;
            public bool ios_appleEnableAutomaticSigning;
            public string ios_manualProvisioningProfileID;
            public string ios_appleDeveloperTeamID;
            public string ios_targetOSVersionString;

            public string android_appID;
            public string android_keystoreName;
            public string android_keystorePass;
            public string android_keyaliasName;
            public string android_keyaliasPass;
#pragma warning restore 0649

            public override string ToString()
            {
                return "companyName : " + companyName +
                "\n productName : " + productName +
                "\n bundleVersion : " + bundleVersion +
                "\n buildNumber : " + buildNumber +
                "\n bundleVersionCode : " + bundleVersionCode +
                "\n applicationDisplayName : " + applicationDisplayName +
                "\n ios_appID : " + ios_appID +
                "\n ios_appleEnableAutomaticSigning : " + ios_appleEnableAutomaticSigning +
                "\n ios_manualProvisioningProfileID : " + ios_manualProvisioningProfileID +
                "\n ios_appleDeveloperTeamID : " + ios_appleDeveloperTeamID +
                "\n ios_targetOSVersionString : " + ios_targetOSVersionString +
                "\n android_appID : " + android_appID +
                "\n android_keystoreName : " + android_keystoreName +
                "\n android_keystorePass : " + android_keystorePass +
                "\n android_keyaliasName : " + android_keyaliasName +
                "\n android_keyaliasPass : " + android_keyaliasPass;
            }
        }
    }
}