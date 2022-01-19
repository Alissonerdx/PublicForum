var mainTabulator;
$(document).ready(function () {
    MainTabulator();
    Modals();
    Events();
    Components();

    function MainTabulator() {

        var actionFormatter = function (cell, formatterParams, onRendered) {
            if (cell.getRow().getData().isOwner == true) {
                return `
                   <div class="btn-group btn-group-sm" role="group" aria-label="Actions">
                      <button type="button" class="btn btn-secondary editTopic" title="Edit" data-id="${cell.getRow().getData().id}" data-title="${cell.getRow().getData().title}" data-author-id=""><i class="fas fa-pencil-alt"></i></button>
                      <button type="button" class="btn btn-secondary showDetails" title="Detail" data-id="${cell.getRow().getData().id}" data-title="${cell.getRow().getData().title}" data-author-id=""><i class="fa fa-eye"></i></button>
                      <button type="button" class="btn btn-secondary deleteTopic" title="Delete" data-id="${cell.getRow().getData().id}" data-title="${cell.getRow().getData().title}" data-author-id=""><i class="fas fa-trash"></i></button>
                    </div>
                `;
            }

            return `
                   <div class="btn-group btn-group-sm" role="group" aria-label="Actions">
                      <button type="button" class="btn btn-secondary showDetails" title="Detail" data-id="${cell.getRow().getData().id}" data-title="${cell.getRow().getData().title}" data-author-id=""><i class="fa fa-eye"></i></button>
                    </div>
            `;
        }

        mainTabulator = new Tabulator("#tabulator", {
            height: "100%",
            layout: "fitColumns",
            addRowPos: "bottom",
            responsiveLayout: true,
            pagination: true,
            paginationMode: "remote",
            paginationSize: 10,
            ajaxURL: "/Topic/GetTableData",
            selectable: 1,
            locale: true,
            columns: [
                { field: "id", visible: false },
                { field: "isOwner", visible: false },
                { title: "TITLE", field: "title", hozAlign: "left", headerFilter: true },
                { title: "AUTHOR", field: "owner", hozAlign: "left", headerFilter: true },
                { title: "DATE", field: "date", hozAlign: "left" },
                { title: "ACTION", formatter: actionFormatter, hozAlign: "center", headerSort: false, headerHozAlign: "center" }
            ],

        });

        mainTabulator.on("renderComplete", function () {
            $('.editTopic').unbind('click');
            $('.editTopic').click(function (e) {
                e.preventDefault();
                let topicId = $(this).attr('data-id');

                $.ajax({
                    type: 'GET',
                    url: `/Topic/GetDetail?Id=${topicId}`,
                    processData: false,
                    contentType: false

                }).done(function (response) {
                    $('#editTopicModal').iziModal('resetContent');

                    $('#editTopicId').val(response.id);
                    $('#editTopicTitle').val(response.title);
                    $('#editTopicDescription').html(response.description);
                    $('#editTopicOwnerId').html(response.owner);

                    $('#editTopicModal').iziModal('open');
                    $('#editTopicModal').iziModal('setTitle', '<span style="color: white; font-weight: 700;">Edit</span>');

                }).fail(function (response) {
                    iziToast.error({
                        id: "error",
                        title: "Error!",
                        message: response.message,
                        position: 'topRight',
                        transitionIn: 'bounceInLeft',
                    });
                });
            });

            $('.showDetails').unbind('click');
            $('.showDetails').click(function (e) {
                e.preventDefault();
                let topicId = $(this).attr('data-id');

                $.ajax({
                    type: 'GET',
                    url: `/Topic/GetDetail?Id=${topicId}`,
                    processData: false,
                    contentType: false

                }).done(function (response) {
                    $('#detailTopicModal').iziModal('resetContent');

                    $('#detailTopicTitle').html(response.title);
                    $('#detailTopicInfo').html(response.info);
                    $('#detailTopicDescription').html(response.description);

                    $('#detailTopicModal').iziModal('open');
                    $('#detailTopicModal').iziModal('setTitle', '<span style="color: white; font-weight: 700;">Detail</span>');

                }).fail(function (response) {
                    iziToast.error({
                        id: "error",
                        title: "Error!",
                        message: response.message,
                        position: 'topRight',
                        transitionIn: 'bounceInLeft',
                    });
                });

            });

            $('.deleteTopic').unbind('click');
            $('.deleteTopic').click(function (e) {
                e.preventDefault();
                let topicId = $(this).attr('data-id');
                let topicTitle = $(this).attr('data-title');

                iziToast.question({
                    layout: 1,
                    overlay: true,
                    displayMode: 1,
                    id: 'question',
                    progressBar: true,
                    title: 'Delete',
                    message: `Do you want to delete the topic ${topicTitle}?`,
                    position: 'center',
                    buttons: [
                        ['<button><b>Cancel</b></button>', function (instance, toast, button, e, inputs) {
                            instance.hide({ transitionOut: 'fadeOut' }, toast, 'button');
                        }, false],
                        ['<button><b>Confirm</b></button>', function (instance, toast, button, e, inputs) {

                            instance.hide({ transitionOut: 'fadeOut' }, toast, 'button');

                            $.post("/Topic/Delete", `id=${topicId}`, function (response) {
                                mainTabulator.setData();

                                if (response.success === true) {
                                    iziToast.success({
                                        id: "success",
                                        title: "Success!",
                                        message: response.message,
                                        position: 'topRight',
                                        transitionIn: 'bounceInLeft',
                                    });
                                } else {
                                    iziToast.error({
                                        id: "error",
                                        title: "Error!",
                                        message: response.message,
                                        position: 'topRight',
                                        transitionIn: 'bounceInLeft',
                                    });
                                }
                            }).fail(function (response) {
                                iziToast.error({
                                    id: "error",
                                    title: "Error!",
                                    message: response.message,
                                    position: 'topRight',
                                    transitionIn: 'bounceInLeft',
                                });
                            });
                        }, true]
                    ]
                });
                //let concursoDescricao = $(this).attr('data-descricao');
                //$('#concursoModal').iziModal('resetContent');
                //$('#concursoModal input[name="Descricao"]').val(concursoDescricao);
                //$('#concursoModal').iziModal('open');
                //$('#concursoModal').iziModal('setTitle', `<span style="color: white; font-weight: 700;">Editar Concurso - ${concursoId}</span>`);
                //$('#concursoModal input[name="Id"]').val(concursoId);
            });
        });

    }

    function Modals() {
        $('#newTopicModal').iziModal({
            width: "50%",
            padding: "10px",
            headerColor: "#1c2f67",
            transitionIn: 'bounceInDown',
            transitionOut: 'bounceOutDown',
            overlayClose: false,
            onOpening: function () {
                $('#newTopicDescription').trumbowyg();
            }
        });

        $('#editTopicModal').iziModal({
            width: "50%",
            padding: "10px",
            headerColor: "#1c2f67",
            transitionIn: 'bounceInDown',
            transitionOut: 'bounceOutDown',
            overlayClose: false,
            onOpening: function () {
                $('#editTopicDescription').trumbowyg();
            }
        });

        $('#detailTopicModal').iziModal({
            width: "50%",
            padding: "10px",
            headerColor: "#1c2f67",
            transitionIn: 'bounceInDown',
            transitionOut: 'bounceOutDown',
            overlayClose: false,
        });
    }

    function Events() {
        $('#newTopic').click(function (e) {
            e.preventDefault();

            $('#newTopicModal').iziModal('resetContent');
            $('#newTopicModal').iziModal('open');
            $('#newTopicModal').iziModal('setTitle', '<span style="color: white; font-weight: 700;">New Topic</span>');
        });


        $(document).on('submit', '#newTopicForm', function (e) {
            e.preventDefault();

            let data = new FormData($(this)[0]);

            $.ajax({
                type: 'POST',
                url: "/Topic/Create",
                data: data,
                processData: false,
                contentType: false

            }).done(function (response) {

                mainTabulator.setData();

                if (response.success === true) {
                    iziToast.success({
                        id: "success",
                        title: "Success!",
                        message: response.message,
                        position: 'topRight',
                        transitionIn: 'bounceInLeft',
                    });
                }
                else {
                    iziToast.error({
                        id: "error",
                        title: "Error!",
                        message: response.message,
                        position: 'topRight',
                        transitionIn: 'bounceInLeft',
                    });
                }
            }).fail(function (response) {
                iziToast.error({
                    id: "error",
                    title: "Error!",
                    message: response.message,
                    position: 'topRight',
                    transitionIn: 'bounceInLeft',
                });
            });

            $('#newTopicModal').iziModal('close');
            e.stopImmediatePropagation();
            return false;
        })

        $(document).on('submit', '#editTopicForm', function (e) {
            e.preventDefault();

            let data = new FormData($(this)[0]);

            $.ajax({
                type: 'POST',
                url: "/Topic/Edit",
                data: data,
                processData: false,
                contentType: false

            }).done(function (response) {
                mainTabulator.setData();

                if (response.success === true) {
                    iziToast.success({
                        id: "success",
                        title: "Success!",
                        message: response.message,
                        position: 'topRight',
                        transitionIn: 'bounceInLeft',
                    });
                }
                else {
                    iziToast.error({
                        id: "error",
                        title: "Error!",
                        message: response.message,
                        position: 'topRight',
                        transitionIn: 'bounceInLeft',
                    });
                }
            }).fail(function (response) {
                iziToast.error({
                    id: "error",
                    title: "Error!",
                    message: response.message,
                    position: 'topRight',
                    transitionIn: 'bounceInLeft',
                });
            });

            $('#editTopicModal').iziModal('close');
            e.stopImmediatePropagation();
            return false;
        })

    }

    function Components() {
        
    }
});