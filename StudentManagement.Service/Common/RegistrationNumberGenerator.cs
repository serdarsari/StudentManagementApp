using StudentManagement.Service.Enums;

namespace StudentManagement.Service.Common
{
    public static class RegistrationNumberGenerator
    {
        public static string Create(RegistrationNumberTypes registrationNumberType, int suffix)
        {
            var newRegistrationNumber = "";

            if (suffix.ToString().Length == 3)
                newRegistrationNumber = registrationNumberType.ToString() + suffix;
            else if (suffix.ToString().Length == 2)
                newRegistrationNumber = registrationNumberType.ToString() + "0" + suffix;
            else if(suffix.ToString().Length == 1)
                newRegistrationNumber = registrationNumberType.ToString() + "00" + suffix;

            return newRegistrationNumber;
        }
    }
}
