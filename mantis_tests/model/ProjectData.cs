using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace mantis_tests
{
    public class ProjectData:IEquatable<ProjectData>,IComparable<ProjectData>
    {

        public string NameProject { get; set; }

        public string Id { get; set; }

        public override int GetHashCode() // сначала сравниваются хэшкоды и если разные, то не равны (оптимизация сравнения)
        {
            // return 0;// если не нужна оптимизация
            return NameProject.GetHashCode();

        }

        public override string ToString()
        {
            return "name = " + NameProject;
        }

        public int CompareTo(ProjectData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return 1;
            }
            return NameProject.CompareTo(other.NameProject);
        }

        public bool Equals(ProjectData other)
        {
            if (Object.ReferenceEquals(other, null))// тот объект с которым мы сравниваем это null
            {
                return false;
            }
            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }

            return NameProject == other.NameProject;
        }
    }
}
