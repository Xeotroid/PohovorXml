namespace BackEnd {
    internal interface IExporter {
        public void SaveTo(List<Employer> inputList, string outputPath);
    }
}
