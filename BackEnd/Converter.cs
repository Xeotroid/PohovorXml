﻿using System;
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
            if(!config.ValidateConfig()) {
                throw new InvalidDataException();
            }
            _deserialised = new();
        }

        public bool Work() {
            Deserialize();
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
    }
}
