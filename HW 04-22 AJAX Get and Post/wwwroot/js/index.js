$(() => {
    
    function refreshPeople() {
        $("#tbl-ppl").empty();

        $.get('/home/GetPeople', ppl => {
            ppl.forEach(p => {
                $("#tbl-ppl").append(
                    `<tr> 
                    <td>${p.firstName}</td>
                    <td>${p.lastName}</td>
                    <td>${p.age}</td>
                    <td><button class="btn btn-success btnedit" data-personid="${p.id}" data-age="${p.age}" data-first="${p.firstName}" data-last="${p.lastName}">Edit</button></td>
                    <td><button class="btn btn-danger btndel" data-personid="${p.id}">Delete</button></td>
                 </tr>`);
            });
        });
    }

    function closeModal() {
        $('.modal').modal('hide');
        $("#first").val('');
        $("#last").val('');
        $("#age").val('');
    }

    /////////////////////////////////////start after index.cshtml loads ////////////////////////////////////////////////////////
    //load all people right up front after initial page is launched (afer index aciton completes)
    refreshPeople();    

    //add a new person
    $("#btn-add").on('click', function () {
  
        $("#modal-ppl").modal();
        $("#modal-title").text('Add');
        $("#modal-update").hide();
        $("#modal-add").show();
        
    });

    $("#modal-add").on('click', function () {
                
        const b = {
            firstName: $("#first").val(),
            lastName: $("#last").val(),
            age: $("#age").val()
        };

        $.post('/home/addperson', b, function () {
            closeModal();
            refreshPeople();
        });

        alert(`${b.firstName} ${b.lastName} added successfully!`);

    })

    //click edit , pre-populate the modal 
    $("#tbl-ppl").on('click', '.btnedit', function () {
       
        const id = $(this).data('personid');
        $("#modal-ppl").modal();
        $("#title").text('Edit');
        $("#modal-add").hide();
        $("#modal-update").show();

        $("#first").val($(this).data('first'));
        $("#last").val( $(this).data('last'));
        $("#age").val($(this).data('age'));
        $("#modal-update").data('personid', id); //add a data attribute to the save/update on the modal so we can use it in the actual update  below. 
     });

    //update after modal
    $("#modal-update").on('click', function () {
        const person = {
            firstName: $("#first").val(),
            lastName: $("#last").val(),
            age: $("#age").val(),
            id: $(this).data('personid'),//it was set above 
                       
        };

        $.post('/home/updateperson', person, function () { 
            closeModal();
            refreshPeople();
        });       
                
    });

    //delete
    $("#tbl-ppl").on('click', '.btndel', function () {
        alert('Are you sure you want to delete?');
        const personId =   $(this).data('personid')
        $.post('/home/deleteperson', { personId }, function () {
            refreshPeople();
        });

    });

     


});