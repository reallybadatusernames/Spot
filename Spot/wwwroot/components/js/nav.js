var MainNavigation = {
    navigation: null,
    active: false,
    toggler: null,
    toggle: function () {
        let self = MainNavigation;
        if (!self.active) {
            self.navigation.classList.add('active');
            self.toggler.classList.add('active');
            self.active = true;
            setTimeout(function () {
                document.getElementsByClassName('page-content-wrapper')[0].addEventListener('click', self.toggle);
            }, 200);
            
        }
        else {
            self.navigation.classList.add('transitioning');
            self.navigation.classList.remove('active');
            self.toggler.classList.remove('active');
            self.active = false;
            setTimeout(function () {
                self.navigation.classList.remove('transitioning');
                document.getElementsByClassName('page-content-wrapper')[0].removeEventListener('click', self.toggle);
            }, 200);
        }
    },
    init: function (toggler) {
        this.navigation = document.getElementById('main-navigation');
        this.toggler = toggler;
        this.toggler.addEventListener('click', this.toggle);
    }
}