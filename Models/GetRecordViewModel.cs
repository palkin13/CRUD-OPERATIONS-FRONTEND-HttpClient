namespace CrudOperation_HttpClient_.Models
{

    public class GetRecordViewModel
    {
        public List<GetRecordDataId> GetRecordData { get; set; }

        public class GetRecordDataId
        {
            public int Id { get; set; }
            public string UserName { get; set; }
            public int Age { get; set; }
        }




    }


}