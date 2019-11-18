using System.Web.Optimization;
using PostMagnet.Web.Backend.Helpers;

namespace PostMagnet.Web.Backend
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            #region Css bundle configuration

            // core css files for all pages (include login page)
            bundles.Add(new StyleBundle("~/Content/css/core").Include(
                "~/Content/global/plugins/font-awesome/css/font-awesome.min.css",
                "~/Content/global/plugins/simple-line-icons/simple-line-icons.min.css",
                "~/Content/global/plugins/bootstrap/css/bootstrap.min.css",
                "~/Content/global/plugins/uniform/css/uniform.default.css",
                "~/Content/global/plugins/bootstrap-switch/css/bootstrap-switch.min.css",
                "~/Content/global/css/components.min.css",
                "~/Content/global/css/plugins.min.css"
            ));

            // addition css files for inner pages
            bundles.Add(new StyleBundle("~/Content/css/inner").Include(
                "~/Content/layouts/layout/css/layout.min.css",
                "~/Content/layouts/layout/css/themes/default.min.css",
                "~/Content/layouts/layout/css/custom.min.css"
            ));

            #endregion

            #region Script bundle configuration

            // core javascript files for all pages (include login page)
            var coreScriptBundles = new ScriptBundle("~/Content/js/core").Include(
                "~/Content/global/plugins/jquery.min.js",
                "~/Content/global/plugins/jquery-ui/jquery-ui.min.js",
                "~/Content/global/plugins/bootstrap/js/bootstrap.min.js",
                "~/Content/global/plugins/bootstrap-hover-dropdown/bootstrap-hover-dropdown.min.js",
                "~/Content/global/plugins/jquery-slimscroll/jquery.slimscroll.min.js",
                "~/Content/global/plugins/jquery.blockui.min.js",
                "~/Content/global/plugins/uniform/jquery.uniform.min.js",
                "~/Content/global/plugins/bootstrap-switch/js/bootstrap-switch.min.js",
                "~/Content/global/scripts/app.min.js"
            );

            coreScriptBundles.Orderer = new BundleOrderer();

            bundles.Add(coreScriptBundles);

            // addition javascript files for inner pages
            bundles.Add(new ScriptBundle("~/Content/js/inner").Include(
                "~/Content/layouts/layout/scripts/layout.min.js",
                "~/Content/layouts/layout/scripts/demo.min.js",
                "~/Content/layouts/global/scripts/quick-sidebar.min.js",
                "~/Content/layouts/global/scripts/share-layout.js",
                "~/Content/layouts/global/scripts/primary-menu.js",
                "~/Content/layouts/global/scripts/top-menu.js"
            ));

            #endregion

            #region dashboard page

            // dashboard css files
            bundles.Add(new StyleBundle("~/Content/css/dashboard").Include(
                "~/Content/global/plugins/bootstrap-daterangepicker/daterangepicker-bs3.css",
                "~/Content/global/plugins/fullcalendar/fullcalendar.min.css"
            ));

            // dashboard external plugin scripts
            bundles.Add(new ScriptBundle("~/Content/js/dashboard_external_plugins").Include(
                "~/Content/global/plugins/bootstrap-daterangepicker/moment.min.js",
                "~/Content/global/plugins/bootstrap-daterangepicker/daterangepicker.js",
                "~/Content/global/plugins/counterup/jquery.waypoints.min.js",
                "~/Content/global/plugins/counterup/jquery.counterup.min.js",
                "~/Content/global/plugins/fullcalendar/fullcalendar.min.js",
                "~/Content/global/plugins/flot/jquery.flot.min.js",
                "~/Content/global/plugins/flot/jquery.flot.resize.min.js",
                "~/Content/global/plugins/flot/jquery.flot.categories.min.js"
            ));

            // dashboard page scripts for administrator
            bundles.Add(new ScriptBundle("~/Content/js/admin_dashboard_page_scripts").Include(
                "~/Content/pages/scripts/admin-dashboard.js"
            ));

            // dashboard page scripts for editor
            bundles.Add(new ScriptBundle("~/Content/js/editor_dashboard_page_scripts").Include(
                "~/Content/pages/scripts/editor-dashboard.js"
            ));

            // dashboard page scripts for contributor
            bundles.Add(new ScriptBundle("~/Content/js/contributor_dashboard_page_scripts").Include(
                "~/Content/pages/scripts/contributor-dashboard.js"
            ));

            #endregion

            #region employee list

            // employee list external plugin scripts
            bundles.Add(new ScriptBundle("~/Content/js/employee_list_external_plugins").Include(
                "~/Content/global/scripts/datatable.js",
                "~/Content/global/plugins/datatables/datatables.min.js",
                "~/Content/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.js",
                "~/Content/global/plugins/moment.min.js",
                "~/Content/global/plugins/bootstrap-daterangepicker/daterangepicker.min.js",
                "~/Content/global/plugins/bootstrap-select/js/bootstrap-select.min.js",
                "~/Content/global/plugins/jquery-validation/js/jquery.validate.min.js",
                "~/Content/global/plugins/jquery-validation/js/additional-methods.min.js",
                "~/Content/global/plugins/bootstrap-pwstrength/pwstrength-bootstrap.min.js",
                "~/Content/global/plugins/jquery-notific8/jquery.notific8.min.js"
            ));

            // employee list page scripts
            bundles.Add(new ScriptBundle("~/Content/js/employee_list_page_scripts").Include(
                "~/Content/pages/scripts/employee-list-managed.js",
                "~/Content/pages/scripts/employee-creation.js",
                "~/Content/pages/scripts/employee-creation-validation.js"
            ));

            #endregion

            #region personal profile

            // personal profile external plugin scripts
            bundles.Add(new ScriptBundle("~/Content/js/employee_profile_external_plugins").Include(
                "~/Content/global/scripts/datatable.js",
                "~/Content/global/plugins/datatables/datatables.min.js",
                "~/Content/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.js",
                "~/Content/global/plugins/select2/js/select2.full.min.js",
                "~/Content/global/plugins/jquery-validation/js/jquery.validate.min.js",
                "~/Content/global/plugins/jquery-validation/js/additional-methods.min.js",
                "~/Content/global/plugins/bootstrap-pwstrength/pwstrength-bootstrap.min.js",
                "~/Content/global/plugins/jquery-notific8/jquery.notific8.min.js"
            ));

            // personal profile page scripts
            bundles.Add(new ScriptBundle("~/Content/js/personal_profile_page_scripts").Include(
                "~/Content/pages/scripts/personal-profile.js",
                "~/Content/pages/scripts/personal-profile-validation.js",
                "~/Content/pages/scripts/personal-notification-managed.js",
                "~/Content/pages/scripts/personal-message-managed.js"
            ));

            #endregion

            #region employee profile

            // employee profile page scripts
            bundles.Add(new ScriptBundle("~/Content/js/employee_profile_page_scripts").Include(
                "~/Content/pages/scripts/employee-profile.js",
                "~/Content/pages/scripts/employee-profile-validation.js"
            ));

            #endregion

            #region log list

            // log list external plugin scripts
            bundles.Add(new ScriptBundle("~/Content/js/log_list_external_plugins").Include(
                "~/Content/global/scripts/datatable.js",
                "~/Content/global/plugins/datatables/datatables.min.js",
                "~/Content/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.js",
                "~/Content/global/plugins/moment.min.js",
                "~/Content/global/plugins/bootstrap-daterangepicker/daterangepicker.min.js",
                "~/Content/global/plugins/bootstrap-select/js/bootstrap-select.min.js"
            ));

            // log list page scripts
            bundles.Add(new ScriptBundle("~/Content/js/log_list_page_scripts").Include(
                "~/Content/pages/scripts/log-list-managed.js"
            ));

            #endregion

            #region website list

            // website list external plugin scripts
            bundles.Add(new ScriptBundle("~/Content/js/website_list_external_plugins").Include(
                "~/Content/global/scripts/datatable.js",
                "~/Content/global/plugins/datatables/datatables.min.js",
                "~/Content/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.js",
                "~/Content/global/plugins/moment.min.js",
                "~/Content/global/plugins/bootstrap-daterangepicker/daterangepicker.min.js",
                "~/Content/global/plugins/bootstrap-select/js/bootstrap-select.min.js",
                "~/Content/global/plugins/jquery-validation/js/jquery.validate.min.js",
                "~/Content/global/plugins/jquery-validation/js/additional-methods.min.js",
                "~/Content/global/plugins/bootstrap-pwstrength/pwstrength-bootstrap.min.js",
                "~/Content/global/plugins/jquery-notific8/jquery.notific8.min.js"
            ));

            // website list page scripts
            bundles.Add(new ScriptBundle("~/Content/js/website_list_page_scripts").Include(
                "~/Content/pages/scripts/website-list-managed.js",
                "~/Content/pages/scripts/website-creation.js",
                "~/Content/pages/scripts/website-creation-validation.js"
            ));

            #endregion

            #region post list

            // post list external plugin scripts
            bundles.Add(new ScriptBundle("~/Content/js/post_list_external_plugins").Include(
                "~/Content/global/scripts/datatable.js",
                "~/Content/global/plugins/datatables/datatables.min.js",
                "~/Content/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.js",
                "~/Content/global/plugins/moment.min.js",
                "~/Content/global/plugins/bootstrap-daterangepicker/daterangepicker.min.js",
                "~/Content/global/plugins/bootstrap-select/js/bootstrap-select.min.js",
                "~/Content/global/plugins/jquery-validation/js/jquery.validate.min.js",
                "~/Content/global/plugins/jquery-validation/js/additional-methods.min.js",
                "~/Content/global/plugins/jquery-notific8/jquery.notific8.min.js"
            ));

            // post list page scripts for administrator
            bundles.Add(new ScriptBundle("~/Content/js/admin_post_list_page_scripts").Include(
                "~/Content/pages/scripts/admin-post.js",
                "~/Content/pages/scripts/admin-post-list-managed.js"
            ));

            // post list page scripts for editor
            bundles.Add(new ScriptBundle("~/Content/js/editor_post_list_page_scripts").Include(
                "~/Content/pages/scripts/editor-post.js",
                "~/Content/pages/scripts/editor-post-list-managed.js"
            ));

            // post list page scripts for contributor
            bundles.Add(new ScriptBundle("~/Content/js/contributor_post_list_page_scripts").Include(
                "~/Content/pages/scripts/contributor-post.js",
                "~/Content/pages/scripts/contributor-post-list-managed.js"
            ));

            #endregion
        }
    }
}
