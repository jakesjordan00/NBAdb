<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GameSearch.aspx.cs" Inherits="NBAdb.GameSearch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Game Search</h2>
    
    <div class="row"> 
        <div class="col-md-3" style="width:fit-content">
            Season Select
            <asp:CheckBox ID="chkSeason" runat="server" OnCheckedChanged="chkSeason_CheckedChanged" AutoPostBack="true" />
            <asp:DropDownList ID="ddSeason" class="btn btn-primary dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" enabled="false"
                runat="server" DataKeyNames="Season" AutoPostBack="true" BackColor="White" BorderColor="Black" Font-Size="Medium" ForeColor="Black">
                <asp:ListItem Text="2024" Value="2024" />
                <asp:ListItem Text="2023" Value="2023" />
                <asp:ListItem Text="2022" Value="2022" />
                <asp:ListItem Text="2021" Value="2021" />
                <asp:ListItem Text="2020" Value="2020" />
        </asp:DropDownList>
        </div>
    </div>
    <div class="row" style="height:10px"> 
    </div>


    <div class="row">  
        <div class="col-md-3" style="width:fit-content">
            Team One:
            <asp:DropDownList 
                ID="ddTeam"  data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" DataKeyNames="Team"
                runat="server" BackColor="White" BorderColor="Black" Font-Size="Medium" ForeColor="Black"
                class="btn btn-primary dropdown-toggle"
                AutoPostBack="true" 
                OnSelectedIndexChanged="ddTeam_SelectedIndexChanged">
            </asp:DropDownList>
        </div>
        <asp:CheckBoxList ID="chkTHoA" runat="server" RepeatDirection="Horizontal" Width="55px" OnSelectedIndexChanged="chkTHoA_SelectedIndexChanged" AutoPostBack="true">
            <asp:ListItem Text="H" Value="H"></asp:ListItem>
            <asp:ListItem Text="A" Value="A"></asp:ListItem>
        </asp:CheckBoxList>
        <asp:CheckBoxList ID="chkTWoL" runat="server" RepeatDirection="Horizontal" Width="55px" OnSelectedIndexChanged="chkTWoL_SelectedIndexChanged" AutoPostBack="true">
            <asp:ListItem Text="W" Value="W"></asp:ListItem>
            <asp:ListItem Text="L" Value="L"></asp:ListItem>
        </asp:CheckBoxList>
    </div>

    <div class="row" style="height:10px"> 
    </div>


    <div class="row">  
        <div class="col-md-3" style="width:fit-content">
            Player:
            <asp:TextBox ID="txtPlayer" runat="server" Width="200px"></asp:TextBox>

        </div>
    </div>

        
    <div class="row" runat="server" id="t2Row" visible="false">
        <div class="col-md-3" style="width:fit-content">
        Team Two:
            <asp:DropDownList 
                ID="ddTeam2"  data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" DataKeyNames="Team"
                runat="server" BackColor="White" BorderColor="Black" Font-Size="Medium" ForeColor="Black"
                class="btn btn-primary dropdown-toggle"
                AutoPostBack="true" 
                OnSelectedIndexChanged="ddTeam2_SelectedIndexChanged">
            </asp:DropDownList>
        </div>
    </div>
   
    <div class="row" style="height:10px"> 
    </div>
        
    <div class="row" runat="server" id="Div1" visible="true">
        <div class="col-md-3" style="width:fit-content">
        Score - 
            T1:<asp:TextBox ID="txtScore1" runat="server" Width="40px"></asp:TextBox>
            T2:<asp:TextBox ID="txtScore2" runat="server" Width="40px"></asp:TextBox>
        </div>
    </div>
   
    <div class="row" style="height:10px"> 
    </div>
    <div class="row">
        <div class="col-md-3" style="width:fit-content">
            Date: 
            <asp:TextBox ID="TextBox1" runat="server" TextMode="Date"></asp:TextBox>
        </div>
    </div>

    
    <div class="row" style="height:10px"> 
    </div>
    
    <div class="row">
        <div class="col-md-3" style="width:fit-content">            
            <asp:Button ID="btnRetrieve" runat="server" Text="Find Games" class="btn btn-primary active" BackColor="Black" 
                ForeColor="White" BorderColor="#ba9653" OnClick="btnRetrieve_Click"/>
        </div>
    </div>
    <asp:PlaceHolder ID="GameLinksPlaceholder" runat="server"></asp:PlaceHolder>

    <asp:Label ID="lblError" runat="server" Text=""></asp:Label>


    <script>
        $(document).ready(function () {
            $("#<%= txtPlayer.ClientID %>").autocomplete({
            source: function (request, response) {
                $.ajax({
                    url: "GameSearch.aspx/GetPlayerNames",
                    type: "POST",
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    data: JSON.stringify({ prefix: request.term }),
                    success: function (data) {
                        response(data.d);
                    },
                    error: function (xhr, status, error) {
                        console.log("Error: " + error);
                    }
                });
            },
            minLength: 2 // Require at least 2 characters before triggering
        });
    });
    </script>
    <style>
        .game-link {
    color: lightblue; /* Default color */
    text-decoration: none;
        }

        .game-link:visited {
            color: grey; /* Change to grey when clicked */
        }
    </style>

    
<%--<script>

    function autocomplete(inp, arr) {
        /*the autocomplete function takes two arguments,
        the text field element and an array of possible autocompleted values:*/
        var currentFocus;
        /*execute a function when someone writes in the text field:*/
        inp.addEventListener("txtPlayer", function (e) {
            var a, b, i, val = this.value;
            /*close any already open lists of autocompleted values*/
            closeAllLists();
            if (!val) { return false; }
            currentFocus = -1;
            /*create a DIV element that will contain the items (values):*/
            a = document.createElement("DIV");
            a.setAttribute("id", this.id + "autocomplete-list");
            a.setAttribute("class", "autocomplete-items");
            /*append the DIV element as a child of the autocomplete container:*/
            this.parentNode.appendChild(a);
            /*for each item in the array...*/
            for (i = 0; i < arr.length; i++) {
                /*check if the item starts with the same letters as the text field value:*/
                if (arr[i].substr(0, val.length).toUpperCase() == val.toUpperCase()) {
                    /*create a DIV element for each matching element:*/
                    b = document.createElement("DIV");
                    /*make the matching letters bold:*/
                    b.innerHTML = "<strong>" + arr[i].substr(0, val.length) + "</strong>";
                    b.innerHTML += arr[i].substr(val.length);
                    /*insert a input field that will hold the current array item's value:*/
                    b.innerHTML += "<input type='hidden' value='" + arr[i] + "'>";
                    /*execute a function when someone clicks on the item value (DIV element):*/
                    b.addEventListener("click", function (e) {
                        /*insert the value for the autocomplete text field:*/
                        inp.value = this.getElementsByTagName("input")[0].value;
                        /*close the list of autocompleted values,
                        (or any other open lists of autocompleted values:*/
                        closeAllLists();
                    });
                    a.appendChild(b);
                }
            }
        });
        /*execute a function presses a key on the keyboard:*/
        inp.addEventListener("keydown", function (e) {
            var x = document.getElementById(this.id + "autocomplete-list");
            if (x) x = x.getElementsByTagName("div");
            if (e.AdCode == 40) {
                /*If the arrow DOWN key is pressed,
                increase the currentFocus variable:*/
                currentFocus++;
                /*and and make the current item more visible:*/
                addActive(x);
            } else if (e.AdCode == 38) { //up
                /*If the arrow UP key is pressed,
                decrease the currentFocus variable:*/
                currentFocus--;
                /*and and make the current item more visible:*/
                addActive(x);
            } else if (e.AdCode == 13) {
                /*If the ENTER key is pressed, prevent the form from being submitted,*/
                e.preventDefault();
                if (currentFocus > -1) {
                    /*and simulate a click on the "active" item:*/
                    if (x) x[currentFocus].click();
                }
            }
        });
        function addActive(x) {
            /*a function to classify an item as "active":*/
            if (!x) return false;
            /*start by removing the "active" class on all items:*/
            removeActive(x);
            if (currentFocus >= x.length) currentFocus = 0;
            if (currentFocus < 0) currentFocus = (x.length - 1);
            /*add class "autocomplete-active":*/
            x[currentFocus].classList.add("autocomplete-active");
        }
        function removeActive(x) {
            /*a function to remove the "active" class from all autocomplete items:*/
            for (var i = 0; i < x.length; i++) {
                x[i].classList.remove("autocomplete-active");
            }
        }
        function closeAllLists(elmnt) {
            /*close all autocomplete lists in the document,
            except the one passed as an argument:*/
            var x = document.getElementsByClassName("autocomplete-items");
            for (var i = 0; i < x.length; i++) {
                if (elmnt != x[i] && elmnt != inp) {
                    x[i].parentNode.removeChild(x[i]);
                }
            }
        }
        /*execute a function when someone clicks in the document:*/
        document.addEventListener("click", function (e) {
            closeAllLists(e.target);
        });
    }

    /*An array containing all the country names in the world:*/
    var players = ["ANGELS ON EARTH",];

    autocomplete(document.getElementById("myInput"), players);
</script>


    
<style>
    * {
        box-sizing: border-box;
    }

    body {
        font: 16px Arial;
    }

    /*the container must be positioned relative:*/
    .autocomplete {
        position: relative;
        display: inline-block;
    }



    .autocomplete-items {
        position: absolute;
        border: 1px solid #d4d4d4;
        border-bottom: none;
        border-top: none;
        z-index: 99;
        /*position the autocomplete items to be the same width as the container:*/
        top: 100%;
        left: 0;
        right: 0;
    }

        .autocomplete-items div {
            padding: 10px;
            cursor: pointer;
            background-color: #fff;
            border-bottom: 1px solid #d4d4d4;
        }

            /*when hovering an item:*/
            .autocomplete-items div:hover {
                background-color: #e9e9e9;
            }

    /*when navigating through the items using the arrow keys:*/
    .autocomplete-active {
        background-color: DodgerBlue !important;
        color: #ffffff;
    }
</style>--%>
</asp:Content>
