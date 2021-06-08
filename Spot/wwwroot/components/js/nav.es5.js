'use strict';

var MainNavigation = {
    navigation: null,
    active: false,
    toggler: null,
    toggle: function toggle() {
        var self = this;
        if (!this.active) {
            self.navigation.classList.add('active');
            self.toggler.classList.add('active');
            self.active = true;
        } else {
            self.navigation.classList.add('transitioning');
            self.navigation.classList.remove('active');
            self.toggler.classList.remove('active');
            self.active = false;
            setTimeout(function () {
                self.nav.classList.remove('transitioning');
            }, 2000);
        }
    },
    init: function init(toggler) {
        this.navigation = document.getElementById('main-navigation');
        this.toggler = toggler;
        this.toggler.addEventListener('click', this.toggle);
    }
};

