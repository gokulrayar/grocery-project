<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OrderHistory.aspx.cs" Inherits="GroceryApp.OrderHistory" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="order-history-container">
        <h4 class="py-3">
            Order History
        </h4>
        <div id="order-history-subcontainer">
            <!-- Orders will be auto injected here by the script -->
        </div>
    </div>
    <style>
    .order-history-card {
        background-color: #f2f3f5;
    }

    .order-history-card-container {
        padding: 20px;
    }

    .product-preview {
        height: 150px;
        width: 150px;
        background-size:cover;
        border-radius: 8px;
    }

    .order-preview-info {
        font-size: 18px;
    }

    .col-2 {
        width: max-content;
    }

    img {
        aspect-ratio: auto;
    }
    </style>
    <script>
        let orders = [
            {
                "orderId": 1,
                "productIds": [1, 2],
                "orderedAt": "2024-01-21"
            },
            {
                "orderId": 2,
                "productIds": [2],
                "orderedAt": "2024-01-21"
            },
            {
                "orderId": 3,
                "productIds": [1],
                "orderedAt": "2024-01-21"
            }
        ];
        let products = [
            {
                "id": 1,
                "name": "English Oven Milk Bread",
                "price": 56,
                "stock": 10,
                "imageUrl": "https://m.media-amazon.com/images/I/61IJuIeXPML.jpg"
            },
            {
                "id": 2,
                "name": "Amul Gold Milk",
                "price": 80,
                "stock": 15,
                "imageUrl": "https://rukminim2.flixcart.com/image/850/1000/ku79vgw0/milk/w/q/b/12-gold-1-ltr-x12-tetrapack-standardized-amul-original-imag7dn2tjymaee8.jpeg?q=20"
            }
        ]

        window.onload = function exec() {
            orders.forEach(order => {
                let root = document.getElementById("order-history-subcontainer");
                var parser = new DOMParser();
                let component =
                `<div class="order-history-card mb-3">
                    <div class="order-history-card-container">
                        <div class="row">
                            <div class="col-2 px-0">
                                <div class="product-preview ms-3" style="background-image:url('${products.find(p => p.id == order.productIds[0]).imageUrl}')"></div>
                            </div>
                            <div class="col-10 my-3 order-preview-info">
                                <div class="row mb-3 ms-2">${order.productIds.length} items were ordered</div>
                                <div class="row mb-3 ms-2">Ordered at ${new Date(order.orderedAt).toDateString()}</div>
                                <div class="row ms-2"><a class="ps-0" href="">Check details here</a></div>
                            </div>
                        </div>
                    </div>
                </div>`;
                var componentHtml = parser.parseFromString(component, "text/html");
                var node = componentHtml.getElementsByClassName('order-history-card')[0];
                root.appendChild(node);
            });
        }
    </script>
</asp:Content>