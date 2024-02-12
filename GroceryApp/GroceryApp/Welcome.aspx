<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Welcome.aspx.cs" Inherits="GroceryApp.Welcome" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="page-content">
        <div class="container mx-2">
            <div class="row">

            </div>
        </div>
    </div>
    <style>
        .container.body-content {
            margin-left: 60px;
            margin-right: 60px;
        }
    </style>
    <script>
        let products;
        function renderProducts() {
            const endpoint = "products";
            const onOk = function (response) {
                products = response.data;
                products.forEach(p => {
                    let parser = new DOMParser();
                    let content = parser.parseFromString(`<div id="$card-${p.id}" class="col-2 mb-3">
                                                              <div class="card shadow-lg" style="width: 200px;">
                                                                  <div class="border" style="background-image: url('${p.imageUrl}'); height: 175px; width: 175px; background-size: cover; margin: 11.5px;"></div>
                                                                  <!--<img src="${p.imageUrl}" class="m-2 border" height="175" width="175" class="card-img-top">-->
                                                                  <div class="card-body pt-0">
                                                                      <h6 class="card-title">${p.name}</h6>
                                                                      <div class="row">
                                                                          <div class="col-5">
                                                                              <a href="#" class="btn btn-primary btn-sm" onclick="javascript:void()"><small>&#x20B9;${p.price}/-</small></a>
                                                                          </div>
                                                                          <div class="col-7 ps-0 text-end">
                                                                              <a href="#" class="btn btn-outline-primary btn-sm cart-btn-${p.id}" onclick="modifyCart(event, 1)"><small class="cart-btn-${p.id}">ADD TO CART</small></a>
                                                                              <a href="#" class="btn btn-outline-danger btn-sm cart-btn-${p.id}" onclick="modifyCart(event, 2)" style="display:"none"};"><small class="cart-btn-${p.id}">REMOVE</small></a>
                                                                          </div>
                                                                      </div>
                                                                   </div>
                                                              </div>
                                                          </div>`, 'text/html')
                    $('#page-content .container .row')[0].append(content.getElementById(`$card-${p.id}`));
                });
                markProducts(products);
            };
            const onError = function (response) {

            };
            send(HttpMethod.GET, endpoint, null, onOk, onError);
        }
    </script>
</asp:Content>
