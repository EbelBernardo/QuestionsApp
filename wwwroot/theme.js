window.theme = {

    set: function (theme) {

        document.body.classList.remove(
            "theme-light",
            "theme-dark"
        );

        document.body.classList.add(theme);

        localStorage.setItem(
            "theme",
            theme
        );

    },


    get: function () {

        return localStorage.getItem("theme")
            ?? "theme-light";

    }

};