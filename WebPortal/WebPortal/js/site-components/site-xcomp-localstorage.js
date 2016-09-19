/*
 SessionStorage support.
 
 Use saveNumber()/loadNumber() to save/load numbers.
 Use saveString()/loadString() to save/load strings.
 Use monitorSelect() to automatically save/load state of select box.
*/

var Site = Site || {};

Site.LocalStorage = Site.LocalStorage || {

    clearAll : function () {
        if (typeof (Storage) !== 'undefined') {
            sessionStorage.clear();
        }
    },

    saveNumber : function (tag, number) {
        if (typeof (Storage) !== 'undefined') {
            sessionStorage.setItem(tag, number.toString());
        }
    },

    loadNumber : function (tag, fallback) {
        if (typeof (Storage) !== 'undefined') {
            var storedvalue = sessionStorage.getItem(tag);
            if (storedvalue == undefined || storedvalue == null) {
                storedvalue = fallback;
            }
            return Number(storedvalue);
        }
        return fallback;
    },

    saveString: function (tag, value) {
        if (typeof (Storage) !== 'undefined') {
            sessionStorage.setItem(tag, value);
        }
    },

    loadString: function (tag, fallback) {
        if (typeof (Storage) !== 'undefined') {
            var storedvalue = sessionStorage.getItem(tag);
            if (storedvalue == undefined || storedvalue == null) {
                storedvalue = fallback;
            }
            return storedvalue;
        }
        return fallback;
    },

    monitorSelect: function (tag, select) {
        if (typeof (Storage) !== 'undefined') {
            var storedvalue = sessionStorage.getItem(tag);
            if (storedvalue != null) {
                select.val(storedvalue);
            }
            select.change(function () {
                sessionStorage.setItem(tag, select.val());
            });
        }
    }
};
