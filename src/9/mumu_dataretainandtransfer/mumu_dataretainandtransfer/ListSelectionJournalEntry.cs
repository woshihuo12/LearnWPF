using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Navigation;
namespace mumu_dataretainandtransfer
{
 //   public delegate void ReplayListChange(ListSelectionJournalEntry state);

    [Serializable()]
    public class ListSelectionJournalEntry : CustomContentState
    {
        private List<String> _sourceItems;
        public List<String> SourceItems
        {
            get { return _sourceItems; }
        }

        private List<String> _targetItems;
        public List<String> TargetItems
        {
            get { return _targetItems; }
        }
        private string journalName;
        

        public ListSelectionJournalEntry(
            List<String> sourceItems, List<String> targetItems,
            string journalName)
        {
            this._sourceItems = sourceItems;
            this._targetItems = targetItems;
            this.journalName = journalName;
            
        }

        public override string JournalEntryName
        {
            get
            {
                return journalName;
            }
        }

        public override void Replay(NavigationService navigationService, NavigationMode mode)
        {
            RegisterPage page = navigationService.Content as RegisterPage;
            if (page == null) return;
            
            page.lstSource.Items.Clear();
            foreach (string item in SourceItems)
            { page.lstSource.Items.Add(item); }

            page.lstTarget.Items.Clear();
            foreach (string item in TargetItems)
            { page.lstTarget.Items.Add(item); }

            page.restoredStateName = JournalEntryName;

        }

    }
}
