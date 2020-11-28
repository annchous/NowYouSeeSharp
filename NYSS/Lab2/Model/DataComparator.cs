using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Office.CustomUI;

namespace Lab2.Model
{
    public static class DataComparator
    {
        public static List<ComparedData> Compare(ObservableCollection<Data> oldCollection,
            ObservableCollection<Data> newCollection)
        {
            var result = new List<ComparedData>();
            for (var i = 0; i < oldCollection.Count; i++)
            {
                if (new CustomComparer().Equals(oldCollection[i], newCollection[i])) continue;
                var name = oldCollection[i].Name != newCollection[i].Name ? newCollection[i].Name : null;
                var description = oldCollection[i].Description != newCollection[i].Description ? newCollection[i].Description : null;
                var source = oldCollection[i].Source != newCollection[i].Source ? newCollection[i].Source : null;
                var target = oldCollection[i].Target != newCollection[i].Target ? newCollection[i].Target : null;
                var confidentialityOffense = oldCollection[i].ConfidentialityOffense != newCollection[i].ConfidentialityOffense ? newCollection[i].ConfidentialityOffense : null;
                var continuityOffense = oldCollection[i].ContinuityOffense != newCollection[i].ContinuityOffense ? newCollection[i].ContinuityOffense : null;
                var availabilityOffense = oldCollection[i].AvailabilityOffense != newCollection[i].AvailabilityOffense ? newCollection[i].AvailabilityOffense : null;

                var changes = new Data
                {
                    Id = newCollection[i].Id,
                    Name = name,
                    Description = description,
                    Source = source,
                    Target = target,
                    ConfidentialityOffense = confidentialityOffense,
                    ContinuityOffense = continuityOffense,
                    AvailabilityOffense = availabilityOffense
                };

                result.Add(new ComparedData(oldCollection[i], changes));
            }

            return result;
        }
    }

    public class CustomComparer : IEqualityComparer<Data>
    {
        public Boolean Equals(Data oldData, Data newData) =>
            (oldData?.Name == newData?.Name
             && oldData?.Description == newData?.Description
             && oldData?.Source == newData?.Source
             && oldData?.Target == newData?.Target
             && oldData?.ConfidentialityOffense == newData?.ConfidentialityOffense
             && oldData?.ContinuityOffense == newData?.ContinuityOffense
             && oldData?.AvailabilityOffense == newData?.AvailabilityOffense);

        public Int32 GetHashCode(Data obj)
        {
            throw new NotImplementedException();
        }
    }
}
