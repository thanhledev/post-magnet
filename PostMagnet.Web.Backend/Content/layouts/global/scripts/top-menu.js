var TopMenu = function () {

    return {

        //main function
        init: function () {
            TopMenu.initNotification();
            TopMenu.initMyInbox();
        },

        initNotification: function () {
            $.ajax({
                url: "/employee/getpendingnotifications"
            }).done(function (data) {
                $("#short-pending-notification").text(data.number);
                $("#full-pending-notification").text(data.number + " pending");
                TopMenu.buildNotificationList(data.list);
            }).fail(function () {
                $.notific8("zindex", 11500);
                $.notific8("Failed!", errorSettings);
            });
        },

        buildNotificationList: function (notifications) {
            var content = '';
            for (var i = 0; i < notifications.length; i++) {
                content += '<li>';
                content += '<a href="javascript:;">';
                content += '<span class="time">' + notifications[i].Created + '</span>';
                content += '<span class="details">';
                
                if (notifications[i].Type === "User") {
                    content += '<span class="label label-sm label-icon label-info"><i class="fa fa-user"></i></span>';
                } else if (notifications[i].Type === "Post") {
                    content += '<span class="label label-sm label-icon label-info"><i class="fa fa-file-text-o"></i></span>';
                } else if (notifications[i].Type === "Invoice") {
                    content += '<span class="label label-sm label-icon label-info"><i class="fa fa-calculator"></i></span>';
                } else {
                    content += '<span class="label label-sm label-icon label-info"><i class="fa fa-rocket"></i></span>';
                }
                content += notifications[i].Content;
                content += '</span>';
                content += '</a>';
                content += '</li>';
            }
            $("#list-pending-notifications").html(content);
        },

        initMyInbox: function() {
            $.ajax({
                url: "/employee/getunreadmessages"
            }).done(function(data) {
                $("#unread-messages").text(data.number);
            }).fail(function() {
                $.notific8("zindex", 11500);
                $.notific8("Failed!", errorSettings);
            });
        }
    };

}();

jQuery(document).ready(function () {
    TopMenu.init();
});
