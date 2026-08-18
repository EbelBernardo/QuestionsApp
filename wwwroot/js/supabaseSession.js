window.supabaseSession = {
    save: function (value) {
        localStorage.setItem("supabase-session", value);
    },

    load: function () {
        return localStorage.getItem("supabase-session");
    },

    remove: function () {
        localStorage.removeItem("supabase-session");
    }
};