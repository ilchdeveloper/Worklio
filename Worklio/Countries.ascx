<%@ control language="C#" autoeventwireup="true" codebehind="Countries.ascx.cs" inherits="Worklio.Countries" %>
<div class="row">
    <div class="col-md-4">
        <div class="form-group">
            <select class="form-control" id="selCountry">
            </select>
            <br>
        </div>
    </div>
    <div class="col-md-4">
        <div class="form-inline hide" id="divCapital">
            <div class="form-group">
                <div style="margin-bottom: 25px" class="input-group">
                    <span class="input-group-addon">Capital:</span>
                    <label id="capital" class="form-control">
                    </label>
                </div>
            </div>
        </div>
    </div>
    <div class="col-md-4">
        <div class="form-group" id="radioLang">
        </div>
    </div>
</div>
<div class="row">
    <div class="panel panel-default hide" id="divTable">
        <!-- Default panel contents -->
        <div class="panel-heading">Bordering countries </div>

        <!-- Table -->
        <table class="table" id="tblBC">
            <thead>
                <tr>
                    <th>#</th>
                    <th>Name</th>
                </tr>
            </thead>
            <tbody>
            </tbody>
        </table>

    </div>
</div>
<script>
    $(document).ready(function () {
        $.ajax({
            url: '/api/launguage',
            type: 'GET',
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            success: function (data, textStatus, xhr) {
                data = data.Result;
                var html = '';
                for (var i = 0; i < data['length']; i++) {
                    html = html + '<label class="radio-inline"><input type="radio" name="optradio" id="radioLang_opt_' + data[i]['Code'] + '"  value="' + data[i]['Code'] + '" />' + data[i]['Code'] + '</label> ';
                }
                $('#radioLang').html(html);
                $('input:radio[name="optradio"][value="en"]').attr('checked', true);
                $.session.set("curlang", "en");
            },
            error: function (xhr, textStatus, errorThrown) {
                console.log('Error in Database');
            }
        });
        $.ajax({
            url: '/api/country',
            type: 'GET',
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            success: function (data, textStatus, xhr) {
                data = data.Result;
                var options = "<option value='-1' selected>Please select country</option>";
                for (var i = 0; i < data['length']; i++) {
                    options += "<option value='" + data[i]['ID'] + "'>" + data[i]['Name'] + "</option>";
                }
                $("#selCountry").append(options);
            },
            error: function (xhr, textStatus, errorThrown) {
                console.log('Error in Database');
            }
        });
        $('#selCountry').change(function () {
            var optionSelected = $(this).find("option:selected");
            var valueSelected = optionSelected.val();
            var textSelected = optionSelected.text();
            var curlng = $('input:radio[name="optradio"]:checked').val();
            if (valueSelected !== '-1') {
                $.ajax({
                    url: '/api/country/' + valueSelected,
                    type: 'GET',
                    dataType: 'json',
                    contentType: "application/json; charset=utf-8",
                    data: { curlang: curlng },
                    success: function (data, textStatus, xhr) {
                        console.log(data.Result.Capital);
                        $('#capital').html(data.Result.Capital);
                        $("#tblBC tbody").html("");
                        var opttr = "";
                        if (data.Result.BorderingCountries.length < 1) {
                            opttr += "<tr><th scope='row'></th><td>Don't nave any bordetng contries</td></tr>"
                        }
                        else {
                            for (var i = 0; i < data.Result.BorderingCountries.length; i++) {
                                opttr += "<tr><th scope='row'>" + (i + 1) + "</th><td>" + data.Result.BorderingCountries[i] + "</td></tr>"
                            }
                        }
                        $("#tblBC tbody").append(opttr);
                    },
                    error: function (xhr, textStatus, errorThrown) {
                        console.log('Error in Database');
                    }
                });
                $('#divTable, #divCapital').removeClass("hide");
            }
            else {
                $('#divTable, #divCapital').addClass("hide");
            }
        });
        $(document).on('change', 'input:radio[id^="radioLang_opt_"]', function (event) {
            var curlng = $(this).val();
            var valueSelected = $("#selCountry option:selected").val();
            if (valueSelected !== '-1') {
                $.ajax({
                    url: '/api/country/' + valueSelected,
                    type: 'GET',
                    dataType: 'json',
                    contentType: "application/json; charset=utf-8",
                    data: { curlang: curlng },
                    success: function (data, textStatus, xhr) {
                        console.log(data.Result.Capital);
                        $('#capital').html(data.Result.Capital);
                        $("#tblBC tbody").html("");
                        var opttr = "";
                        if (data.Result.BorderingCountries.length < 1) {
                            opttr += "<tr><th scope='row'></th><td>Don't nave any bordetng contries</td></tr>"
                        }
                        else {
                            for (var i = 0; i < data.Result.BorderingCountries.length; i++) {
                                opttr += "<tr><th scope='row'>" + (i + 1) + "</th><td>" + data.Result.BorderingCountries[i] + "</td></tr>"
                            }
                        }
                        $("#tblBC tbody").append(opttr);
                    },
                    error: function (xhr, textStatus, errorThrown) {
                        console.log('Error in Database');
                    }
                });
            }
        });
    });
</script>