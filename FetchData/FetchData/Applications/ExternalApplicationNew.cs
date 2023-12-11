
#region NameSpace
using Autodesk.Revit.DB.Events;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Autodesk.Revit.ApplicationServices;
using System.IO;
using System.Windows.Markup;
#endregion


namespace FetchData.Applications
{
    /// <summary>
    /// Setup whole plugins interface with tabs, panels, buttons,...
    /// </summary>
    /// <seealso cref="Autodesk.Revit.UI.IExternalApplication"/>
    public class ExternalApplicationNew : IExternalApplication
    {

        #region Constructor
        /// <summary>
        /// Default Constructor
        /// Initializes a new instance of the <see cref="ExternalApplicationNew"/>
        /// </summary>
        public ExternalApplicationNew()
        {

        }
        #endregion

        #region Public methods

        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }

        /// <summary>
        /// Initializes all interface elements on custom created Revit tab.
        /// </summary>
        /// <param name="application">The application.</param>
        /// <returns></returns>
        public Result OnStartup(UIControlledApplication application)
        {
            #region Family Manager

            //Variables
            string RibbonTabName = "ENR Steel";

            //Create Ribbon
            application.CreateRibbonTab(RibbonTabName);

            // Button Image
            Uri showFamilyManagerPath = new Uri("pack://application:,,,/FamilyManager;component/Images/showFamily.png");
            BitmapImage showFamilyManage = new BitmapImage(showFamilyManagerPath);

           // Uri hideFamilyManagerPath = new Uri("pack://application:,,,/FamilyManager;component/Images/hideFamily.png");
           // BitmapImage hideFamilyManage = new BitmapImage(hideFamilyManagerPath);

            //get command dll file path
            string path = Assembly.GetExecutingAssembly().Location;

            //Create Button
            PushButtonData showFamilyManageButton = new PushButtonData("Show Family", "Family Manager", path, "FamilyManager.Applications.RevitCommand.FamilyManager")
            {
                LargeImage = showFamilyManage,
                ToolTip = "allow user to manage Family in the Project"
            };
     
            //Create Panel
            RibbonPanel Panel = application.CreateRibbonPanel(RibbonTabName, "QA/QC");

            Panel.AddItem(showFamilyManageButton);
          
            application.ControlledApplication.ApplicationInitialized += DockablePanelRegister;

            // add line to separation Ribbon panel
            Panel.AddSeparator();

            #region Create Stacked Items

            // PushButton for Revision Purge
            Uri RevisionPurgePath = new Uri("pack://application:,,,/FamilyManager;component/Images/Purge.png");
            BitmapImage RevisionPurge = new BitmapImage(RevisionPurgePath);
            PushButtonData pushButtonDataRevisionPurge = new PushButtonData("Revision Purge", "Revision Purge", path, "FamilyManager.Applications.RevitCommand.RevisionPurge")
            {
                ToolTip = "deletes unused revisions from the project",
                Image = RevisionPurge,
                LongDescription = "This add-in deletes unused revisions from the project by comparing the list of all revisions defined in the project against the list of revisions shown on all sheets in the project",
            };

            //  Set Url for help to using this ribbon panel
            ContextualHelp contextualHelpRevisionPurge = new ContextualHelp(ContextualHelpType.Url, "https://www.revitapidocs.com/");
            pushButtonDataRevisionPurge.SetContextualHelp(contextualHelpRevisionPurge);
                        

            // PushButton for Fetch Data
            Uri FetchDataPath = new Uri("pack://application:,,,/FamilyManager;component/Images/FetchData.png");
            BitmapImage FetchData = new BitmapImage(FetchDataPath);
            PushButtonData pushButtonDataFetchData = new PushButtonData("Fetch Data", "Fetch Data", path, "FamilyManager.Applications.RevitCommand.FetchData")
            {
                ToolTip = "Fetching data to extract information from the model",
                Image = FetchData,
                LongDescription = "This add-ins allow to data extraction and can access and manipulate the data in a structured manner. This is a powerful and flexible for Fetching data.",
            };

            //  Set Url for help to using this ribbon panel
            ContextualHelp contextualHelpFetchData = new ContextualHelp(ContextualHelpType.Url, "https://www.revitapidocs.com/");
            pushButtonDataFetchData.SetContextualHelp(contextualHelpFetchData);

            Panel.AddStackedItems(pushButtonDataRevisionPurge, pushButtonDataFetchData);
                                   
            #endregion

            #region SQL Data

            //Create Panel
            RibbonPanel DataSQLPanel = application.CreateRibbonPanel(RibbonTabName, "Data Sync");

            // Button Image
            Uri ImagePath = new Uri("pack://application:,,,/FamilyManager;component/Images/Import&Export.png");
            BitmapImage NameImage = new BitmapImage(ImagePath);

            //Create Button
            PushButtonData pushButtonDataSQL = new PushButtonData("SQLData", "SQLData", path, "FamilyManager.Applications.RevitCommand.SQLData")
            {
                LargeImage = NameImage,
                ToolTip = "Export and import Data to a SQL Database",
                LongDescription = "Create a sql table which we can export and import Data to a SQL Database",

            };

            //  Set Url for help to using this ribbon panel
            ContextualHelp contextualHelpDataSQL = new ContextualHelp(ContextualHelpType.Url, "https://www.revitapidocs.com/");
            pushButtonDataSQL.SetContextualHelp(contextualHelpDataSQL);

            DataSQLPanel.AddItem(pushButtonDataSQL);

            #endregion

            return Result.Succeeded;

            #endregion
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Register dockable panel in zero state doc
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">The <see cref="ApplicationInitializedEventArgs"/>instance containing the event data.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        private void DockablePanelRegister(object sender, ApplicationInitializedEventArgs e)
        {
            // Implement the logic code here.
        }

        /// <summary>
        /// Checks if Preferences prefs.cbb File exists
        /// </summary>
        /// <returns></returns>
        private bool PreferencesFileExists()
        {
            // Check if prefs.cbb file exists in assembly location folder.
            return true;
        }

        #endregion

    }
}



