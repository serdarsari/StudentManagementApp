
namespace StudentManagement.Service.Common
{
    public static class CommonFunctions
    {
        public static int GetCurrentSemester()
        {

            if (DateTime.Now.Month <= 12 && DateTime.Now.Month >= 9)
                return 1;
            else if (DateTime.Now.Month >= 1 && DateTime.Now.Month <= 6)
                return 2;

            return 0;
        }
    }
}
