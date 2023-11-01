namespace BackEnd {
    public class Config {
        public List<string> InputPaths { get; private set; }
        public string? OutputPath { get; set; }
        //možno dále rozšířit o další věci, které jsou pak passnuté
        //backendu - např. separator char pro CSV nebo výběr mezi
        //exportem do CSV a XLSX.

        public Config() {
            InputPaths = new();
        }

        public bool AddInputFile(string path) {
            //zkontrolovat, zda už soubor není v seznamu
            //pokud ano, jednoduše return
            //pokud ne, přidat
            if (InputPaths.Contains(path)) {
                return false;
            }
            InputPaths.Add(path);
            return true;
        }

        public void RemoveInputFile(string path) {
            //jednoduchý remove
            InputPaths.Remove(path);
        }
    }
}