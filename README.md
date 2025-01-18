# Customizable Forms Web Application

## Overview

This project is a web application developed using .NET Blazor Server for customizable forms, such as quizzes, tests, questionnaires, and polls.
Inspired by Google Forms, it allows users to create templates with various question types and enables other users to fill out these forms.
The application is fully featured with user management, template management, form submission.

## Features

### General Features

- **Template Management**: Users can create, edit, and delete form templates.
- **Form Submission**: Authenticated users can fill out forms based on available templates.
- **Admin Capabilities**: Admins have full control over all templates and forms.
- **User Authentication**: Registration and login functionalities for users.
- **Responsive Design**: Adaptive UI for desktop and mobile devices with light and dark themes.
- **Integration**: Salesforce integration for user management via REST API.
- **Multilingual Support**: UI available in English and Russian languages. (partially)
- TODO: **Search**: Full-text search to find templates based on questions, descriptions, or comments.

### Template Features

- **Custom Questions**: Single-line text, multi-line text, positive integers, and checkboxes.
- **Template Settings**: Title, description (Markdown support), topic selection, optional image upload, tags with autocomplete, and access control.
- **Question Reordering**: Drag-and-drop functionality to reorder questions.
- TODO: - **Result Aggregation**: View aggregated data such as average values or most common answers.

### Admin Features

- **User Management**: View, block/unblock, delete users, and manage admin privileges.
- **Form Access**: Admins can view and edit any form or template.

### Additional Functionalities

- **Likes and (TODO: Comments)**: Users can like templates and (TODO: add comments with real-time updates).
- **Data Analytics**: Admins and template creators can analyze form submissions.
- **Tag Cloud**: Visual representation of popular tags (TODO: with linked search results).

## Technical Details

### Tech Stack

- **Frontend**: Blazor Server with MudBlazor for UI components.
- **Backend**: .NET with Entity Framework Core for data access.
- **Database**: PostgreSQL.
- **Search Engine**: Full-text search implementation.
- **Authentication**: ASP.NET Core Identity.
- **Integration**: Salesforce REST API for CRM functionalities.

### Deployment

This application is deployed using Docker and designed for continuous deployment,
ensuring consistent and reliable deployment across different environments.
You can access the live application [here](https://forms-gjr1.onrender.com).

## Installation

1. Clone the repository:

   ```bash
   git clone https://github.com/Viorbrint/forms
   ```

2. Navigate to the project directory:

   ```bash
   cd forms
   ```

3. Restore dependencies and run the application:

   ```bash
   dotnet restore
   dotnet run
   ```

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.
