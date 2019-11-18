using System;
using PostMagnet.Domain.Entities;

namespace PostMagnet.Web.Backend.Helpers
{
    public class LogBuilderHelper
    {
        #region Employee

        public static string EmployeeLoginLog(string employeeUsername, string ip, bool status, int failedCount)
        {
            return status
                ? string.Format("{0} has logged successfully from {1}", employeeUsername, ip)
                : string.Format("{0} failed when trying to login from {1} at {2} times", employeeUsername, ip,
                    failedCount);
        }

        public static string EmployeeLogOut(string employeeUsername, string ip)
        {
            return string.Format("{0} has logged out successfully from {1}", employeeUsername, ip);
        }

        public static string PersonalUpdateProfileLog(string employeeUsername, Employee updateProfile)
        {
            return string.Format("{0} has updated his/her personal profile to value {1}", employeeUsername,
                updateProfile);
        }

        public static string PersonalUpdatePasswordLog(string employeeUsername)
        {
            return string.Format("{0} has updated his/her password", employeeUsername);
        }

        public static string EmployeeCreationLog(string employeeUsername, Employee newEmployee)
        {
            return string.Format("{0} has created a new employee with value {1}", employeeUsername, newEmployee);
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
            return string.Format("{0} has reset {1}'s password successfully", employeeUsername, resetEmployeeUsername);
        }

        public static string EmployeeUpdateRate(string employeeUsername, string updateEmployeeUsername, int oldCost, int newCost)
        {
            return string.Format("{0} has update {1}'s article cost from {2} to {3}", employeeUsername,
                updateEmployeeUsername, oldCost, newCost);
        }

        #endregion

        #region Website

        public static string WebsiteCreationLog(string employeeUsername, Website newWebsite)
        {
            return string.Format("{0} has created a new website with value {1}", employeeUsername, newWebsite);
        }

        public static string WebsiteUpdateLog(string employeeUsername, Website updateWebsite)
        {
            return string.Format("{0} has update website with a new value {1}", employeeUsername, updateWebsite);
        }

        #endregion

        #region 3rd Parties

        public static string ThirdPartyUpdateLog(string employeeUsername, string partyName, string newValue)
        {
            return string.Format("{0} has update third party name: {1} to a new value : {2}", employeeUsername, partyName, newValue);
        }

        #endregion

        #region Post

        public static string ApprovePostLog(string employeeUsername, string code, bool confirmation, string submission, string host, string category, string scheduleDate)
        {
            string logContent = string.Empty;

            if (confirmation)
            {
                logContent = string.Format("{0} has approved post's code {1}", employeeUsername, code);

                if (string.Compare(submission, "Yes", StringComparison.InvariantCultureIgnoreCase) == 0)
                {
                    logContent += string.Format(", and choose to post it to website {0} on category {1} right away.", host, category);
                }
                if (string.Compare(submission, "Schedule", StringComparison.InvariantCultureIgnoreCase) == 0)
                {
                    logContent +=
                        string.Format(", and choose to post it to website {0} on category {1} at {2}.", host, category,
                            scheduleDate);
                }
            }
            else
            {
                logContent = string.Format("{0} has denied post's code {1}", employeeUsername, code);
            }

            return logContent;
        }

        public static string ChangeSchedulePostLog(string employeeUsername, string code, string submission, string host,
            string category, string scheduleDate)
        {
            string logContent = string.Empty;

            if (string.Compare(submission, "No", StringComparison.InvariantCultureIgnoreCase) == 0)
            {
                logContent = string.Format("{0} has removed post's code {1} from schedule list", employeeUsername, code);
            }
            else if (string.Compare(submission, "Yes", StringComparison.InvariantCultureIgnoreCase) == 0)
            {
                logContent = string.Format("{0} has posted post's code {1} to website {2} on category {3} right away", employeeUsername, code, host, category);
            }
            else
            {
                logContent = string.Format("{0} has updated post's code {1} to website {2} on category {3} at {4}", employeeUsername, code, host, category, scheduleDate);
            }

            return logContent;
        }

        #endregion
    }
}