$(document).ready(function () {
    var a = [{
        text: "Parent 1", href: "#parent1", tags: ["4"],
        nodes: [{
            text: "Child 1", href: "#child1", tags: ["2"], nodes: [{
                text: "Grandchild 1", href: "#grandchild1",
                tags: ["0"]
            }, { text: "Grandchild 2", href: "#grandchild2", tags: ["0"] }]
        },
        { text: "Child 2", href: "#child2", tags: ["0"] }]
    },
        { text: "Parent 2", href: "#parent2", tags: ["0"] },
        { text: "Parent 3", href: "#parent3", tags: ["0"] },
    { text: "Parent 4", href: "#parent4", tags: ["0"] },
    { text: "Parent 5", href: "#parent5", tags: ["0"] }],
    b = [{
        text: "Parent 1", tags: ["2"], nodes: [{
            text: "Child 1", tags: ["3"], nodes: [{ text: "Grandchild 1", tags: ["6"] },
                { text: "Grandchild 2", tags: ["3"] }]
        }, { text: "Child 2", tags: ["3"] }]
    }, { text: "Parent 2", tags: ["7"] },
    { text: "Parent 3", icon: "icon-location4", href: "#demo", tags: ["11"] },
    { text: "Parent 4", icon: "icon-cloud-download3", href: "/index.html", tags: ["19"], selected: !0 },
    {
        text: "Parent 5", icon: "icon-speech-bubble", color: "#FFF", backColor: "#e8273a", href: "http://www.pixinvent.com",
        tags: ["available", "0"]
    }], c = '[{"text": "Parent 1","nodes": [{"text": "Child 1","nodes": [{"text": "Grandchild 1"},{"text": "Grandchild 2"}]},{"text": "Child 2"}]},{"text": "Parent 2"},{"text": "Parent 3"},{"text": "Parent 4"},{"text": "Parent 5"}]'; $("#default-treeview").treeview({ data: a }),
    $("#collapsed-treeview").treeview({ levels: 1, data: a }), $("#expanded-treeview").treeview({ levels: 99, data: a }), $("#primary-color-treeview").treeview({ color: "#967ADC", data: a }), $("#custom-icon-treeview").treeview({ color: "#967ADC", expandIcon: "icon-ios-arrow-forward", collapseIcon: "icon-ios-arrow-down", nodeIcon: "icon-bookmark2", data: a }), $("#tags-badge-treeview").treeview({ color: "#967ADC", expandIcon: "icon-stop22", collapseIcon: "icon-checkbox-unchecked", nodeIcon: "icon-head", showTags: !0, data: a }), $("#no-border-treeview").treeview({ color: "#967ADC", showBorder: !1, data: a }), $("#colourful-treeview").treeview({ expandIcon: "icon-stop22", collapseIcon: "icon-checkbox-unchecked", nodeIcon: "icon-head", color: "#FFF", backColor: "#3BAFDA", onhoverColor: "#1cade0", borderColor: "#1cade0", showBorder: !1, showTags: !0, highlightSelected: !0, selectedColor: "#FFF", selectedBackColor: "#1cade0", data: a }), $("#node-override-treeview").treeview({ expandIcon: "icon-stop22", collapseIcon: "icon-checkbox-unchecked", nodeIcon: "icon-head", color: "#FFF", backColor: "#3BAFDA", onhoverColor: "#1cade0", borderColor: "#1cade0", showBorder: !1, showTags: !0, highlightSelected: !0, selectedColor: "#FFF", selectedBackColor: "#1cade0", data: b }), $("#link-enabled-treeview").treeview({ color: "#967ADC", enableLinks: !0, data: a }); var d = $("#disabled-treeview").treeview({ data: a, onNodeDisabled: function (a, b) { $("#disabled-output").prepend("<p>" + b.text + " was disabled</p>") }, onNodeEnabled: function (a, b) { $("#disabled-output").prepend("<p>" + b.text + " was enabled</p>") }, onNodeCollapsed: function (a, b) { $("#disabled-output").prepend("<p>" + b.text + " was collapsed</p>") }, onNodeUnchecked: function (a, b) { $("#disabled-output").prepend("<p>" + b.text + " was unchecked</p>") }, onNodeUnselected: function (a, b) { $("#disabled-output").prepend("<p>" + b.text + " was unselected</p>") } }), e = function () { return d.treeview("search", [$("#input-disable-node").val(), { ignoreCase: !1, exactMatch: !1 }]) }; e(); $("#btn-disable-all").on("click", function (a) { d.treeview("disableAll", { silent: $("#chk-disable-silent").is(":checked") }) }), $("#btn-enable-all").on("click", function (a) { d.treeview("enableAll", { silent: $("#chk-disable-silent").is(":checked") }) }); var f = ($("#data-treeview").treeview({ data: c }), $("#searchable-tree").treeview({ color: "#967ADC", showBorder: !1, data: a })), g = function (a) { var b = $("#input-search").val(), c = { ignoreCase: $("#chk-ignore-case").is(":checked"), exactMatch: $("#chk-exact-match").is(":checked"), revealResults: $("#chk-reveal-results").is(":checked") }, d = f.treeview("search", [b, c]), e = "<p>" + d.length + " matches found</p>"; $.each(d, function (a, b) { e += "<p>- " + b.text + "</p>" }), $("#search-output").html(e) }; $("#btn-search").on("click", g), $("#input-search").on("keyup", g), $("#btn-clear-search").on("click", function (a) { f.treeview("clearSearch"), $("#input-search").val(""), $("#search-output").html("") }); var h = function () { return $("#selectable-tree").treeview({ data: a, color: "#967ADC", showBorder: !1, multiSelect: $("#chk-select-multi").is(":checked") }) }, i = h(), j = function () { return i.treeview("search", [$("#input-select-node").val(), { ignoreCase: !1, exactMatch: !1 }]) }, k = j(); $("#chk-select-multi:checkbox").on("change", function () { console.log("multi-select change"), i = h(), k = j() }), $("#input-select-node").on("keyup", function (a) { k = j(), $(".select-node").prop("disabled", !(k.length >= 1)) }), $("#btn-select-node.select-node").on("click", function (a) { i.treeview("selectNode", [k]) }),
    $("#btn-unselect-node.select-node").on("click", function (a) { i.treeview("unselectNode", [k]) }), $("#btn-toggle-selected.select-node").on("click", function (a) { i.treeview("toggleNodeSelected", [k]) }); var l = $("#expandible-tree").treeview({ data: a }); $("#btn-expand-all").on("click", function (a) { $("#select-expand-all-levels").val(); l.treeview("expandAll", { levels: 2 }) }), $("#btn-collapse-all").on("click", function (a) { l.treeview("collapseAll") });
    var m = $("#checkable-tree").treeview({ data: a, showIcon: !1, showCheckbox: !0 }); $("#btn-check-all").on("click",
        function (a) { m.treeview("checkAll", { silent: $("#chk-check-silent").is(":checked") }) }),
    $("#btn-uncheck-all").on("click",
        function (a) { m.treeview("uncheckAll", { silent: $("#chk-check-silent").is(":checked") }) })
});