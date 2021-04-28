# DocumentLibrary
A simple e-library web application for lending books

Some services available in the Azure free tier could be used to implement some of the functionality required.

Basic web application for management of book loaning must be implemented.

Requirements:

1. Administration UI – users with administrator rights should be able to upload or delete books (txt, docx, pdf, etc..). There should be an option to specify book genre, set of keywords and free text description at the time of upload. Uploaded documents should be stored in Azure storage.
Books checked out by a user cannot be deleted.

2. Public Library UI – any user can access the list of all books stored in the library. For each book in the list the following information should be presented
    - Name of the book (same as the file name)
    - Genre
    - Keywords
    - Description
    - Availability date.
    - Link for download (if not checked out by another user)
    
    Users should be allowed to perform search for books based on match in any or the following the following:
    
    - Name
    - Content of book
    - Genre
    - Keywords.

Users should be able to download a book after the book is checked out. Books checked out by other users should be unavailable for download. Checkout period is 1 week. After this period books can be considered as available for checkout by other users.

NOTE: Data for added or deleted books should be immediately updated and search results should reflect those changes immediately.
