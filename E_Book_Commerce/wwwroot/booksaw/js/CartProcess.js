

$(document).ready(function () {


    function AddCart() {
        alert("------------------")
        debugger;
        $.ajax({
            //js ile db işlemleri yapar
            //sepet için cookie, session ya da db de sepet tablosu da yapılabilir
            url: "/Cart/AddCart",//Controller altında method a gidecek
            type: "POST",
            data: { "bookId": bookId },//"bookId" (çift tırnak içinde bookId ile AddCart methodunda parametre olmalı)
            success: function () {

                debugger;
                //<a href="~/Cart/CartIndex" class="cart for-buy">
                //    <i class="icon icon-clipboard"></i><p id="cartQuantityId">0</p> Adet<span>
                //        Cart:( <p id="cartPriceId">0</p>
                //        TL)
                //    </span>
                //</a>
                //

                var getQuantity = document.getElementById("cartQuantityId");
                var getPrice = document.getElementById("cartPriceId").value;



            }




        });

    }
    function AddCart(bookId) {
        debugger;
        $.ajax({
            //js ile db işlemleri yapar
            //sepet için cookie, session ya da db de sepet tablosu da yapılabilir
            url: "/Cart/AddCart",//Controller altında method a gidecek
            type: "POST",
            data: { "bookId": bookId },//"bookId" (çift tırnak içinde bookId ile AddCart methodunda parametre olmalı)
            success: function () {

                debugger;
                //<a href="~/Cart/CartIndex" class="cart for-buy">
                //    <i class="icon icon-clipboard"></i><p id="cartQuantityId">0</p> Adet<span>
                //        Cart:( <p id="cartPriceId">0</p>
                //        TL)
                //    </span>
                //</a>
                //

                var getQuantity = document.getElementById("cartQuantityId");
                var getPrice = document.getElementById("cartPriceId").value;



            },
            error: function () {

            }

        
        });



    }


});