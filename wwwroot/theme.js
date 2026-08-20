window.theme = {

    DEFAULT: "theme-dark",

    set: function (theme) {

        if (theme !== "theme-light" && theme !== "theme-dark") {
            theme = window.theme.DEFAULT;
        }

        document.body.classList.remove("theme-light", "theme-dark");
        document.body.classList.add(theme);

        var meta = document.querySelector('meta[name="theme-color"]');
        if (meta) {
            meta.setAttribute("content", theme === "theme-light" ? "#F5EFE5" : "#16120E");
        }

        try {
            localStorage.setItem("theme", theme);
        } catch (e) {
            /* modo privado ou storage bloqueado: segue sem persistir */
        }
    },

    get: function () {

        var stored = null;

        try {
            stored = localStorage.getItem("theme");
        } catch (e) { }

        if (stored !== "theme-light" && stored !== "theme-dark") {
            stored = window.theme.DEFAULT;
        }

        return stored;
    }
};

/* Aplica imediatamente, antes de o Blazor subir. */
window.theme.set(
    document.documentElement.getAttribute("data-boot-theme") || window.theme.get()
);