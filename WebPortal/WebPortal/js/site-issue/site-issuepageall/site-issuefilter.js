var Site = Site || {};

Site.IssueFilter = function (createCB, viewCB, filterCB) {
    this.NO_VIEW       = 0;
    this.CALENDAR_VIEW = 1;
    this.SCHEDULE_VIEW = 2;
    this.LIST_VIEW     = 3;

    var thiz    = this;
    this.view   = this.NO_VIEW;
    this.filter = $('#filter-issue-panel');

    this.getCurrentView = function() {
        return this.view;
    }

    this.setCurrentView = function(view) {
        this.view = view;
        viewCB();
    }

    this.viewCalendar = function () {
        return this.view == this.CALENDAR_VIEW;
    }

    this.viewSchedule = function () {
        return this.view == this.SCHEDULE_VIEW;
    }

    this.viewList = function () {
        return this.view == this.LIST_VIEW;
    }

    this.noCustomerSelected = function() {
        return this.getCustomerSelection() == '0';
    }

    this.getCustomerSelection = function () {
        return this.filter.find('#customer-select').val();
    }

    this.getStatusSelection = function () {
        return this.filter.find('#status-select').val();
    }

    this.getPrioritySelection = function () {
        return this.filter.find('#priority-select').val();
    }

    this.getAreaTypeSelection = function () {
        return this.filter.find('#areatype-select').val();
    }

    this.setCreateButtonState = function (customerid) {
        this.filter.find('#create-issue-button').attr('disabled', customerid == '0');
    }

    this.filter.find('#calendar-view-select').click(function () {
        thiz.view = thiz.CALENDAR_VIEW;
        viewCB();
    });

    this.filter.find('#schedule-view-select').click(function () {
        thiz.view = thiz.SCHEDULE_VIEW;
        viewCB();
    });

    this.filter.find('#list-view-select').click(function () {
        thiz.view = thiz.LIST_VIEW;
        viewCB();
    });

    this.filter.find('#customer-select').change(function () {
        thiz.setCreateButtonState($(this).val());
        filterCB();
    });

    this.filter.find('#areatype-select').change(function () {
        filterCB();
    });

    this.filter.find('#status-select').change(function () {
        filterCB();
    });

    this.filter.find('#priority-select').change(function () {
        filterCB();
    });

    this.filter.find('#create-issue-button').click(function () {
        if (thiz.getCustomerSelection() !== '0') {
            var today = Site.DatePicker.getCurrentDate();
            var atnow = Site.DatePicker.getCurrentTime();
            createCB(today, atnow, today, atnow, '0');
        }
    });

    this.setCreateButtonState('0');
    siteAjaxGetSilent('/Customer/GetAllCompanyCustomers', null, function (data) {
        Site.Select.load(thiz.filter.find('#customer-select'), data);
    });
}