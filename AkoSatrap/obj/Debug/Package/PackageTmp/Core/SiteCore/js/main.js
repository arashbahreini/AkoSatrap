// Sidebar
function sidebar(activeItem) {
    $('[data-toggle="offcanvas"]').click(function () {
        $('.row-offcanvas').toggleClass('active')
    });
    var menu_list_group_item = $('.menu-list-group-item');
    menu_list_group_item.on('show.bs.collapse', function () {
        $(".list-group-item:first .fa-chevron-down", $(this)).addClass("fa-rotate-180");
    }).on('hide.bs.collapse', function () {
        $(".list-group-item:first .fa-chevron-down", $(this)).removeClass("fa-rotate-180");
    });
    if (activeItem >= 0) {
        menu_list_group_item.removeClass('active');
        menu_list_group_item.eq(activeItem).addClass('active');
    }
}

// control form messages
function ui_validation_state(input_id, state, message) {
    var _this = $(input_id),
        form_group = _this.closest(".form-group"),
        is_file = _this.is("input[type='file']"),
        is_checkbox = _this.is("input[type='checkbox']"),
        is_radio = _this.is("input[type='radio']"),
        is_select = _this.is("select"),
        input_group_parent = _this.parent("div.input-group"),
        is_input_group = input_group_parent.length;

    _this.siblings(".form-control-feedback").remove();
    input_group_parent.siblings(".form-control-feedback").remove();

    if (is_checkbox || is_radio) {
        _this.closest("label").removeClass("has-success has-warning has-danger").addClass(function () {
            if (state)
                return "has-" + state;
        });
        return;
    } else {
        form_group.removeClass("has-success has-warning has-danger").addClass(function () {
            if (state)
                return "has-" + state;
        });
    }

    if (is_file) {
        _this.next(".custom-file-control").removeClass("form-control-success form-control-warning form-control-danger").addClass(function () {
            if (state)
                return "form-control-" + state;
        });
    }
    else if (!is_select && !is_input_group) {
        _this.removeClass("form-control-success form-control-warning form-control-danger").addClass(function () {
            if (state)
                return "form-control-" + state;
        });
    }

    if (message) {
        var _message = $('<div class="form-control-feedback">' + message + '</div>');
        if (is_input_group) {
            _message.insertAfter(input_group_parent);
        } else {
            _message.insertAfter(_this);
        }
    }
}

// Alert
function make_alert(state, message, dismissible, limit, location) {
    if (limit == true || state == "clear") {
        $(".alert-row").remove();
    }
    var _alert;
    if (dismissible == true) {
        _alert = $('<div class="row alert-row"><div class="col"><div class="alert alert-dismissible fade show alert-' + state + '" role="alert"><i class="fa fa-' + state + ' fa-fw ml-3" aria-hidden="true"></i>' + message + '<button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button></div></div></div>');
        $(".alert", _alert).on('closed.bs.alert', function () {
            _alert.remove();
        })
    } else if (dismissible == false) {
        _alert = $('<div class="row alert-row"><div class="col"><div class="alert alert-' + state + '" role="alert"><i class="fa fa-' + state + ' fa-fw ml-3" aria-hidden="true"></i>' + message + '</div></div></div>');
    } else {
        return;
    }
    if (location) {
        _alert.prependTo($(location));
    } else {
        _alert.insertAfter($("body #page-content .page-header"))
    }
}

// File input setup
function setup_file_input() {
    $(':file').change(function () {
        var _this = $(this),
            _numFiles = _this.get(0).files.length,
            _this_form_group = _this.closest('.form-group'),
            _text_location = _this_form_group.find('.custom-file-control'),
            _image_location = _this_form_group.find('img');

        if (_numFiles > 0) {
            var _label = _this.val().replace(/\\/g, '/').replace(/.*\//, ''),
                _log = _numFiles > 1 ? _numFiles + ' files selected' : _label,
                reader = new FileReader();
            _text_location.addClass("filled").text(_log);

            reader.onload = function (e) {
                _image_location.attr('src', e.target.result).removeClass('hidden-xs-up');
                _this_form_group.addClass('has-img');
            };
            reader.readAsDataURL(this.files[0]);

        } else {
            _text_location.removeClass("filled").text('');
            _image_location.attr('src', '').addClass('hidden-xs-up');
            _this_form_group.removeClass('has-img');
        }
    })
}