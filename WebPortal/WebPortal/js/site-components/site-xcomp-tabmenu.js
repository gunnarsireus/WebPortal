/*
 Bootstrap tabmenu support.

 Markup:

   <div class="row site-tabmenumargin-bottom">
     <div class="col-md-12">
       <ul class="nav nav-tabs">
         <li role="presentation" class="active tabmenu"><a href="#">Transitions</a></li>   <-- Note "tabmenu" class, needed!
         <li role="presentation" class="       tabmenu"><a href="#">Location</a></li>
         <li role="presentation" class="       tabmenu"><a href="#">Team</a></li>
       </ul>
     </div>
   </div>
   <div class="row" id="workorder-transitions-page" hidden>                                <-- Note div ID
     <div class="col-md-10 col-md-offset-1">
       @Html.Partial("WorkOrderTransitionTable")
     </div>
   </div>
   <div class="row" id="..." hidden>
     <div class="col-md-10 col-md-offset-1">
       @Html.Partial("...")
     </div>
  </div>

Initialization:

  var tabmenu = null;
  ...
  function loadTransitionsPage() {                                                         <-- Note function
    // This callback is called by tabmenu when the corresponding tab is clicked
    // *and* if the tab is marked as dirty. Before the first callback, a tab is
    // considered dirty. After the first callback the tab is considered clean.
    // The callback won't be called unless the tab is manually set as dirty.
  }
  ...
  tabmenu = new Site.TabMenu();
  tabmenu.init([
    ['#workorder-transitions-page', loadTransitionsPage],                                  <-- Note div ID and function
    ['...', ...],
    ['...', ...]
  ]);
  
  // Make sure callback is called for next click on tab with index 0 
  tabmenu.setDirty(0, true);
*/

var Site = Site || {};

Site.TabMenu = Site.TabMenu || {};

Site.TabMenu = function () {
    this.dirtyflags = null;

    this.init = function (tabarray) {
        this.dirtyflags = [];
        for (var i = 0; i < tabarray.length; i++) {
            if (i == 0) {
                var p = tabarray[i][0];
                $(p).show();
            }
            this.dirtyflags.push(true);
        }
        var dirtyarray = this.dirtyflags;
        var lastitem = null;
        $('.tabmenu').click(function () {
            if (!$(this).is(lastitem)) {
                lastitem = $(this);
                var idx = -1;
                var ul = $(this).parent();
                ul.find('li').each(function (i) {
                    var li = $(this);
                    if ($(this).is(lastitem)) {
                        $(this).addClass('active');
                        idx = i;
                    }
                    else {
                        $(this).removeClass('active');
                        var p = tabarray[i][0];
                        $(p).hide();
                    }
                });
                if (idx >= 0) {
                    var p = tabarray[idx][0];
                    $(p).show();
                    var f = tabarray[idx][1];
                    if (f != null && dirtyarray[idx] == true) {
                        dirtyarray[idx] = false;
                        f();
                    }
                }
            }
        });
    }

    this.isDirty = function (tabindex) {
        return this.dirtyflags[tabindex] = true;
    }

    this.setDirty = function (tabindex, state) {
        this.dirtyflags[tabindex] = state;
    }
};
