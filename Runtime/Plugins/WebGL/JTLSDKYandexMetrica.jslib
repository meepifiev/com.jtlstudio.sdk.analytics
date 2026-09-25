mergeInto(LibraryManager.library, {
    JTLSDKMetricaInitialize: function (counter) {
        var id = parseInt(UTF8ToString(counter));

        if (!id) {
            return;
        }

        window.jtlsdkMetricaCounter = id;

        (function (m, e, t, r, i, k, a) {
            m[i] = m[i] || function () { (m[i].a = m[i].a || []).push(arguments) };
            m[i].l = 1 * new Date();
            for (var j = 0; j < document.scripts.length; j++) { if (document.scripts[j].src === r) { return; } }
            k = e.createElement(t), a = e.getElementsByTagName(t)[0], k.async = 1, k.src = r, a.parentNode.insertBefore(k, a)
        })(window, document, "script", "https://mc.yandex.ru/metrika/tag.js", "ym");

        ym(id, "init", {
            clickmap: false,
            trackLinks: true,
            accurateTrackBounce: true
        });

        var noscript = document.createElement("noscript");
        noscript.innerHTML = '<div><img src="https://mc.yandex.ru/watch/' + id + '" style="position:absolute; left:-9999px;" alt="" /></div>';
        document.body.appendChild(noscript);
    },

    JTLSDKMetricaReachGoal: function (name, dataJson) {
        try {
            if (typeof ym !== "function" || !window.jtlsdkMetricaCounter) {
                console.warn("[JTL SDK Analytics] Yandex Metrica is not ready yet.");
                return;
            }

            var goal = UTF8ToString(name);
            var json = UTF8ToString(dataJson);
            var data = json ? JSON.parse(json) : null;

            if (data && Object.keys(data).length > 0) {
                ym(window.jtlsdkMetricaCounter, "reachGoal", goal, data);
            } else {
                ym(window.jtlsdkMetricaCounter, "reachGoal", goal);
            }
        } catch (error) {
            console.error("[JTL SDK Analytics] goal failed:", error);
        }
    }
});
