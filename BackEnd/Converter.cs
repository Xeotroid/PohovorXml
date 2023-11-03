global using System;
global using System.Collections.Generic;
global using System.Linq;
global using System.Text;
using System.Xml.Serialization;

namespace BackEnd {
    public class Converter {
        private Config _config;
        private List<Employer> _deserialised;
        private static readonly log4net.ILog _log = LogHelper.GetLogger();

        public Converter(Config config) {
            _config = config;
            _deserialised = new();
        }

        /// <summary>
        /// Provede deserializaci, transformaci a uložení dat do výstupního souboru.
        /// </summary>
        /// <exception cref="InvalidDataException"></exception>
        public void Work() {
            if (!_config.ValidateConfig()) {
                throw new InvalidDataException();
            }
            _log.Info($"Započata konverze do {_config.OutputPath}.");
            Deserialize();
            FilterUnemployed();
            MergeEmployers();
            SortAll();
            AddParentReferences();
            var exporter = (IExporter)Activator.CreateInstance(_config.Exporter)!;
            exporter.SaveTo(_deserialised, _config.OutputPath!);
        }

        //private:

        private void Deserialize() {
            foreach (string path in _config.InputPaths) {
                using Stream reader = new FileStream(path, FileMode.Open);
                var serializer = new XmlSerializer(typeof(Employer));
                try {
                    var employer = (Employer?)serializer.Deserialize(reader);
                    if (employer != null) {
                        _deserialised.Add(employer);
                    }
                }
                catch {
                    _log.Error($"Chyba při serializaci souboru {path}.");
                    continue;
                }
            }
        }

        private void FilterUnemployed() {
            foreach (Employer employer in _deserialised) {
                employer.RemoveAll(employee => employee.IsUnemployed);
            }
        }

        private void MergeEmployers() {
            List<Employer> merged = new();
            foreach (Employer emp in _deserialised) {
                Employer? matching = merged.Find(x => x.CompanyName == emp.CompanyName);
                if (matching == null) {
                    merged.Add(emp);
                } else {
                    matching.Employees = matching.Employees.Concat(emp.Employees).ToList();
                }
            }
            _deserialised = merged;
        }

        private void SortAll() {
            foreach (Employer employer in _deserialised) {
                employer.Sort();
            }
            _deserialised.Sort();
        }

        private void AddParentReferences() {
            foreach (Employer employer in _deserialised) {
                if (employer.Employees == null) continue;
                foreach (Employee employee in employer.Employees) {
                    employee.ParentEmployer = employer;
                    if (employee.Address == null) continue;
                    employee.Address.ParentEmployee ??= employee;
                }
            }
        }
    }
}
