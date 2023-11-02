using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BackEnd {
    public class Converter {
        private Config _config;
        private List<Employer> _deserialised;

        public Converter(Config config) {
            if (!config.ValidateConfig()) {
                throw new InvalidDataException();
            }
            _config = config;
            _deserialised = new();
        }

        public bool Work() {
            Deserialize();
            FilterUnemployed();
            MergeEmployers();
            SortBoth();
            AddEmployerReferences();
            IExporter exporter = new CsvExporter();
            exporter.SaveTo(_deserialised, _config.OutputPath);
            return true;
        }

        //private:

        private void Deserialize() {
            foreach (string path in _config.InputPaths) {
                using Stream reader = new FileStream(path, FileMode.Open);
                var serializer = new XmlSerializer(typeof(Employer));
                var employer = (Employer)serializer.Deserialize(reader);
                _deserialised.Add(employer);
            }
        }

        private void FilterUnemployed() {
            foreach (Employer employer in _deserialised) {
                employer.Employees = employer.Employees.Where(x => !string.IsNullOrEmpty(x.EmployedSince)).ToList();
            }
        }

        private void MergeEmployers() {
            List<Employer> merged = new();
            foreach (Employer emp in _deserialised) {
                Employer matching = merged.Find(x => x.CompanyName == emp.CompanyName);
                if (matching == null) {
                    Debug.Print($"Not found, adding");
                    merged.Add(emp);
                } else {
                    Debug.Print($"Found, merging");
                    matching.Employees = matching.Employees.Concat(emp.Employees).ToList();
                }
            }
            _deserialised = merged;
        }

        private void SortBoth() {
            foreach (Employer employer in _deserialised) {
                employer.Employees.Sort();
            }
            _deserialised.Sort();
        }

        private void AddEmployerReferences() {
            foreach (Employer employer in _deserialised) {
                foreach (Employee employee in employer.Employees) {
                    employee.Company = employer;
                }
            }
        }
    }
}
