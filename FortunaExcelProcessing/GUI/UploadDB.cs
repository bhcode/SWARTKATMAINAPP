using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FortunaExcelProcessing.DBExtras;
using System.IO;

namespace FortunaExcelProcessing.GUI
{
    public class UploadDB
    {
        bool lab = false, hive = false, weekly = false, obs = false;

        public bool UploadAll()
        {
            //Task.Factory.StartNew(() => { UploadLabels(); });
            //Task.Factory.StartNew(() => { UploadHives(); });
            //Task.Factory.StartNew(() => { UploadWeeklyData(); });
            Task.Factory.StartNew(() => { UploadObservations(); });

            while (lab != true && hive != true && weekly != true && obs != true) {}

            return true;
        }

        public void UploadLabels()
        {
            foreach (Label lab in ReadDB.LoadLabelData())
            {
                UploadData.UploadLabel(lab);
            }
            lab = true;
        }

        public void UploadHives()
        {
            foreach(Hive hive in ReadDB.LoadHiveData())
            {
                UploadData.UploadHive(hive);
            }
            hive = true;
        }

        public void UploadWeeklyData()
        {
            foreach(WeeklyData data in ReadDB.LoadWeeklyData())
            {
                UploadData.UploadWeeklyData(data);
            }
            weekly = true;
        }

        public void UploadObservations()
        {
            foreach (Observation observation in ReadDB.LoadObservationData())
            {
                UploadData.UploadObservation(observation);
            }
            obs = true;
        }
    }
}
