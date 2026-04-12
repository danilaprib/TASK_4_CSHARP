namespace TASK_4_CSHARP.Data
{
    public class User
    {
        public int user_id { get; set; }

        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string status { get; set; }
         
        public bool is_blocked{ get; set; }
    }
}
