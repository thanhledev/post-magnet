using System;
using PostMagnet.Domain.Entities;

namespace PostMagnet.Web.Backend.Helpers
{
    public class NotificationBuilderHelper
    {
        #region Employee

        public static string EmployeeCreationNotification(string employeeUsername, Employee newEmployee)
        {
            return string.Format("{0} has created a new {1}", employeeUsername, newEmployee.Role.Name);
        }

        public static string EmployeeUpdateAccessibility(string employeeUsername, string updateEmployeeUsername,
            bool status)
        {
            return status
                ? string.Format("{0} has reactived {1}'s access privilege", employeeUsername, updateEmployeeUsername)
                : string.Format("{0} has banned {1}'s access privilege", employeeUsername, updateEmployeeUsername);
        }

        public static string EmployeeResetPassword(string employeeUsername, string resetEmployeeUsername)
        {
            return string.Format("{0} has reset {1}'s password", employeeUsername, resetEmployeeUsername);
        }

        public static string EmployeeUpdateRate(string employeeUsername, string updateEmployeeUsername)
        {
            return string.Format("{0} has update {1}'s article cost", employeeUsername, updateEmployeeUsername);
        }

        #endregion

        #region Post

        public static string ApprovePostNotification(string employeeUsername, string code, bool confirmation)
        {
            return confirmation
                ? string.Format("{0} has approved post's code {1}", employeeUsername, code)
                : string.Format("{0} has denied post's code {1}", employeeUsername, code);
        }

        public static string ApproveAndDeliveryPostNotification(string employeeUsername, string code)
        {
            return string.Format("{0} has approved post's code {1} and posted it.", employeeUsername, code);
        }

        public static string ApproveAndSchedulePostNotification(string employeeUsername, string code)
        {
            return string.Format("{0} has approved post's code {1} and scheduled it.", employeeUsername, code);
        }

        public static string UpdateSchedulePostNotification(string employeeUsername, string code, string submission)
        {
            if (string.Compare(submission, "No", StringComparison.InvariantCultureIgnoreCase) == 0)
            {
                return string.Format("{0} has removed post's code {1} from schedule list", employeeUsername, code);
            }
            if (string.Compare(submission, "Yes", StringComparison.InvariantCultureIgnoreCase) == 0)
            {
                return string.Format("{0} has removed post's code {1} schedule & posted it right away", employeeUsername, code);
            }
            return string.Format("{0} has updated post's code {1} schedule", employeeUsername, code);
        }

        #endregion
    }
}