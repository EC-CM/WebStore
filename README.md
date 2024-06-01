# Tech Store Website

![Tech Store Home Page](https://github.com/EC-CM/WebStore/assets/114674192/85a78a9e-0dff-4d35-bb82-f32d88618ccd)

The final product successfully serves as a prototype website, providing a functional representation of a hypothetical tech store which implements a database. It has basic login functionality, although not fully implemented, and full functionality to interact with the database backend of products, lists and users. 

>**Scenario:** Create a prototype website on a chosen topic – utilising a database backend. It should:
>
>​      • Allow users to browse and search information.
>
>​      •	Have a log-in system for users.
>
>​      •	Allow users to add/edit/delete items.
>
>​      •	Adhere to legal and ethical standards.
>
>​      •	Full stack created using ASP.NET, MVC, JavaScript, C# and SQL.

&nbsp;

There are more features on this site than below, but these are some key ones.


## Navigation Bar
![Website-NavBar](https://github.com/EC-CM/WebStore/assets/114674192/ae2511c6-d30c-4542-bc5f-e750f5e0632b)

## Product List
![Website-ProductListings](https://github.com/EC-CM/WebStore/assets/114674192/8e1359b3-e510-40c1-8063-9e247c4322af)
  **(1)** The result of a user search for Sony.
  
  **(2)** Various sort options, which can be applied on top of any search filters.
   
  **(3)** A favourites button, which also adds this product to the user’s favourites list in the database. The favourite status of each product persists across the site, even upon a refresh.
   
  **(4)** The option to add an item to the user’s cart, interacting with the database. This also updates to show if an item is already in the cart upon page load.
   
  **(5)** A link to show all products from the same category as that product.

&nbsp;

## Product Details
![Website-Details](https://github.com/EC-CM/WebStore/assets/114674192/0c218d98-b317-41ae-9aab-32d9f2a5597e)

The details page includes more information on a product, which is mainly its text description and any relevant images in an image carousel. The user can also add the item to their cart from this page, but not to their favourites. Links are also present to view all products in that category and to return to the product list page, but the edit button does not work.

## Favourites
![Website-Favourites](https://github.com/EC-CM/WebStore/assets/114674192/f7879d3b-d50f-4f4d-926c-134dc564490c)

The user’s favourites, which users can easily add to and remove from by just pressing the favourites button **(2)** on this page or in product listings. A timestamp **(1)** also shows when the product was favourited, although there are no sort options for this. As seen with **(3)**, users can store notes for each product by simply clicking the notes field, which by default looks like **(5)**, revealing an input field **(4)**.
