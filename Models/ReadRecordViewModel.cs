namespace CrudOperation_HttpClient_.Models
{
    public class ReadRecordViewModel
    {
        public List<ReadRecordData> readRecordData { get; set; }

        public class ReadRecordData
        {
            public int Id { get; set; }
            public string UserName { get; set; }
            public int Age { get; set; }
        }

    }
}
