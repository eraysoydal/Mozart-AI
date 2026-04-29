Akdeniz University

Department of Computer Engineering

Software Engineering Project

Project short-name: Mozart AI

Software Requirements Specification

Team Leader: Ramazan Şahin

Product Owner: Ramazan Şahin, Eray Soydal

Instructor: Prof. Ümit Deniz ULUŞAR

01.03.2026

This report is submitted to the Department of Computer Engineering of Akdeniz University of the Software Engineering course CSE332.

| Abbreviations | | | --- | | --- | | IP | Internet Protocol | | AI | Artificial Intelligence | | SRS | Software Requirements Specification | | API | Application Programming Interface | | HTTP | Hyper Text Transfer Policy | | HLS | HTTP Live Streaming | | JSON | Java Script Object Notation | | JWT | JSON Web Token | | RBAC | Role-Based Access Control | | TBD | To Be Determined | | SQL | Structured Query Language | | UI | User Interface | | UX | User Experience | | | | | | | | | | | | | | | | | | | | | | | | | | | |

Contents

Introduction 1

Purpose 1
Product Scope 1
References 1
Overall Description 1

Product Perspective 1
Product Functions 1
User Types and Characteristic 2
Operating Environment 2
Client-Side (Mobile) Environment 2
2.4.2 Server-Side (Backend) Environment 2

2.4.3 AI Processing Environment (The Worker Core) 2

2.4.4 Coexistence and Connectivity 2

2.5 Design and Implementation Constraints 3

2.5.1 Regulatory and Corporate Policies 3

2.5.2 Hardware and Timing Limitations 3

2.5.3 Specific Technologies and Tools 3

2.5.4 Communication Protocols and Parallel Operations 3

2.5.5 Security Considerations 3

2.5.6 Design Conventions and Standards 3

2.6 User Documentation 4

2.6.1 Frequently Asked Questions 4

2.6.2 Tutorials and Onboarding 4

2.6.3 Delivery Formats and Standards 4

2.7 Assumptions and Dependencies 4

2.7.1 Assumptions 4

2.7.2 Dependencies 4

External Interface Requirements 5

User Interfaces 5
Hardware Interfaces 5
Software Interfaces 5
Communications Interfaces 6
System Features / Requirements 6

1 User Class: Listener (General User) 6
4.2 User Class: Artist 6

4.2.1 Feature: Original Content Upload 6

4.3 User Class: Administrator 7

4.3.1 Feature: Artist Verification 7

4.4 Functional Requirements 7

4.4.1 User Class: Listener(General User) 7

4.4.2 User Class: Artist 9

4.4.3 User Class: Administor 9

4.5 Error Handling & Invalid Inputss 10

Nonfunctional System Requirements 10

Other Requirements 10

System Models 10

7.1 System Context 10

7.2 Interaction Models 11

7.2.1Use-Case Models 11

7.3 System Models 12

7.3.1. Sequence Diagrams 13

7.4 Structural Models 14

7.4.1. Object and Class Model 14

7.5. Behavioral Models 15

7.5.1. Data Driven (Activity Diagrams) 15

7.5.2. Data Driven (Sequence Diagrams) 16

7.5.3. Event Driven Models (State Diagrams) 18

7.6 User Interface 19

8 References 23

Appendix A: Glossary 23
Team Members, Roles and CVs 25
Introduction
Purpose
The purpose of this document is to define the software requirements for Mozart-AI, an AI-powered music platform. The application allows users to discover, stream, and interact with music tracks. A unique aspect of Mozart-AI is its integration of generative AI features, allowing users to generate music tracks based on prompts, moods, and instruments. The platform also serves as a social network for music enthusiasts, supporting artist followings, likes, comments, and track sharing, while providing artists with detailed analytics and a comprehensive dashboard.

Product Scope
The Mozart-AI platform consists of a cross-platform frontend client (mobile and web) and a robust backend API. Features encompass user authentication, profile management, music playback, generative AI track creation, playlist management, and social interactions (following, liking, commenting). Additionally, the system includes an artist application workflow and a content moderation interface for administrators.

References
IEEE Recommended Practice for Software Requirements Specifications

Object-Oriented Software Engineering Using UML, Patterns, and Java

Material Design 3 Guidelines

Microsoft .NET Documentation

AudioCraft: MusicGen Documentation

Microsoft. (2024). ASP.NET Core Documentation. https://docs.microsoft.com/en-us/aspnet/core

Microsoft. (2024). Entity Framework Core Documentation. https://docs.microsoft.com/en-us/ef/core

Google. (2024). Flutter & Dart Documentation. https://flutter.dev/docs

Martin, R. C. (2017). Clean Architecture: A Craftsman's Guide to Software Structure and Design. Prentice Hall.

IEEE. (1998). IEEE Std 830-1998: Recommended Practice for Software Requirements Specifications.

User Interface Sigma Design -> https://www.figma.com/design/EmxtxO1WB3SExrsFMViMKe/SE-project-prototype?m=auto&t=okuHSEVA7eS0LwSz-6

Overall Description
Product Perspective
Mozart AI is a next-generation music ecosystem that integrates generative artificial intelligence with traditional streaming services. While it serves as a modern alternative to existing music players, it is a new member of the "AI-native" application family rather than a follow-on to a specific existing system.

The system architecture follows a client-server model centered around a .NET API, which manages the communication between the Flutter mobile client, the AI Generation Worker, and the System Administrative Portal.

Product Functions
User Authentication: Secure login for listeners, artists, and admins.
Artist Verification: A dedicated workflow where System Admins review and approve artist applications.
AI Prompt Builder: A structured interface for all users to create remixes from reference tracks.
Social Interaction: Threaded comments (nested) and social sharing of generated content.
Content Management: Artists can upload tracks and define whether their music can be used for AI remixing.
User Types and Characteristic
System Admin: Responsible for verifying artist accounts and moderating the social feed.
Artist: Users who have been verified by the Admin to upload original content.
Listener: General users who stream music, create AI remixes, and interact with the community.
Operating Environment
Mozart AI operates within a distributed ecosystem consisting of a mobile client, a high-performance backend server, and a dedicated AI processing unit. The software must peacefully coexist with various platform-specific media services and background tasks.

Client-Side (Mobile) Environment
The mobile application is developed using the Flutter framework to provide a cross-platform experience across different mobile operating systems.

Operating Systems: The application is compatible with Android 11.0 (API Level 30) or higher and iOS 15.0 or higher.
Hardware Platform: A minimum of 4 GB of RAM is required to manage real-time HLS audio decoding, waveform visualization, and the asynchronous AI generation workflow.
Network Requirements: A stable internet connection (4G/5G or Wi-Fi) is required for q streaming and AI remixing;
2.4.2 Server-Side (Backend) Environment
The centralized server manages the core logic, including artist verification, database operations, and user authentication.

Operating System: The backend is hosted on a Linux-based server (Ubuntu 22.04 LTS) or Windows Server 2025 environment.
Framework: The system requires the .NET 9.0 Runtime to execute the primary API services.
Database Management: The server environment must include MS SQL for relational data storage (users, nested comments, and track metadata) and Redis for managing the AI job queue.
Software Interoperability: The backend must coexist with S3-compatible object storage services for handling high-fidelity audio files and HLS segments.
2.4.3 AI Processing Environment (The Worker Core)
Due to the intensive computational nature of Suno-like music generation, the AI core operates in a specialized third party APIs

2.4.4 Coexistence and Connectivity
Mozart AI is designed to integrate harmoniously with the user's existing device ecosystem:

Media Services: The app must be able to coexist with global system tasks, allowing background audio playback while the user interacts with other applications.
Security Protocols: Communication between the mobile client and the .NET backend must be conducted over HTTP/S (TLS 1.3) to ensure data privacy and integrity.
Artist Verification: The system requires a web browser interface (Chrome, Safari, or Edge) for System Admins to access the administrative dashboard and verify artist applications.
2.5 Design and Implementation Constraints
2.5.1 Regulatory and Corporate Policies
AI Content Labeling: To comply with emerging digital content standards, every track generated by the AI must be explicitly labeled as "AI-Generated" in the metadata and user interface.
Derivative Work Rights: The system must maintain a strict link between a generated remix and its original reference track to ensure proper attribution to the original artist.
Artist Verification Policy: No user can upload original content or manage AI permissions until their identity and "Artist" status are manually verified by a System Admin.
2.5.2 Hardware and Timing Limitations
AI Inference Timing: The music generation process must be optimized to provide a result within 60 seconds to maintain user engagement.
Mobile Memory Requirements: The Flutter application must operate within a 150 MB RAM limit during active HLS streaming to ensure stability on mid-range devices.
2.5.3 Specific Technologies and Tools
Backend Framework: As per course requirements, the backend must be implemented using C# and .NET 9.0.
Frontend Framework: Flutter will be used for cross-platform mobile development to ensure a unified codebase for iOS,Android and Web.
Database Systems: PostgreSQL is mandatory for storing relational data such as user profiles and nested comment hierarchies, while Redis will handle the asynchronous job queue for AI tasks.
2.5.4 Communication Protocols and Parallel Operations
Streaming Protocol: All online audio delivery must utilize HLS (HTTP Live Streaming) to allow for adaptive bitrate streaming and reduced initial buffering.
Asynchronous Processing: The AI generation must run as a parallel background operation. The .NET backend will use a "fire-and-forget" or "task-queue" pattern to ensure the user can continue browsing while a remix is being composed.
2.5.5 Security Considerations
Role-Based Access Control (RBAC): The system must strictly separate permissions for Admins (verification), Artists (upload/permissions), and Listeners (streaming/remixing).
Authentication: User sessions must be managed using JWT (JSON Web Tokens) with a maximum expiration period of 24 hours for security.
2.5.6 Design Conventions and Standards
Clean Architecture: The .NET backend should follow a layered architecture (Domain, Application, Infrastructure, API) to ensure the software is easy to maintain and test.
UI/UX Standards: The application will follow Material Design 3 conventions to provide a familiar and intuitive navigation experience similar to industry-standard music applications.
Language Requirements: The system must support Internationalization (i18n), with English as the primary development language and Turkish as the secondary locale.
2.6 User Documentation
To ensure a seamless user experience across all user classes, a comprehensive set of documentation and help components will be delivered alongside the Mozart AI mobile and web platforms.

2.6.1 Frequently Asked Questions
Frequently Asked Questions (FAQ): A categorized list of questions addressing account management, AI remixing credits, and artist verification timelines.
2.6.2 Tutorials and Onboarding
Interactive Onboarding Walkthrough: A series of guided screens for first-time users explaining how to use the AI Prompt Builder and how to navigate the Generated Feed.

Artist Video Tutorial: A short, screen-recorded guide for verified artists demonstrating the track upload process, metadata entry, and AI permission toggling.

Admin Manual: A PDF-format guide specifically for System Administrators detailing the steps for reviewing identity proofs and managing the artist verification queue.

2.6.3 Delivery Formats and Standards
Mobile-Responsive Web Help: The primary documentation will be hosted on a web server, accessible via a "Help" link within the mobile app's profile page.
PDF Documentation: Formal manuals (Admin and Technical guides) will be delivered in Portable Document Format (PDF) to ensure consistent formatting across devices.
Markdown README: Technical documentation for the .NET backend and API endpoints will be provided in Markdown (.md) format within the project repository for developer reference.
2.7 Assumptions and Dependencies
2.7.1 Assumptions
Device Performance: It is assumed that the application will be used on mobile devices that meet the minimum specified hardware requirements (4 GB RAM). If the device lacks sufficient available resources-for instance, if the user has heavily allocated them to other background applications-the software may fail to perform as intended or crash.
User Connectivity: We assume that users will have access to a stable 4G/5G or Wi-Fi connection for core features such as AI generation and HLS streaming.
Artist Integrity: It is assumed that users applying for "Artist" status will provide authentic credentials for the System Admin to review, ensuring the platform's library remains professional and legitimate.
2.7.2 Dependencies
Third-Party AI API Availability: The project is critically dependent on the availability and uptime of external Generative AI APIs (e.g., Suno or Meta MusicGen API). Any changes to their API structure, pricing models, or service availability will directly impact the "Remix" functionality of Mozart AI.
Cross-Platform Framework (Flutter): The project relies on the continued support and stability of the Flutter SDK and its community-contributed packages for audio playback (just_audio) and networking (dio)
Backend Runtime (.NET 9.0): The system depends on the .NET 9.0 Runtime and associated NuGet packages (such as Entity Framework Core) for managing the database and API logic.
Media Processing Tools: The backend is dependent on FFmpeg for segmenting audio files into HLS-compatible chunks and managing file transcoding.
External Interface Requirements
User Interfaces
Home Feed: Displays original and generated tracks with "Download" and "Remix" icons.

Artist Application Portal: A form where users submit credentials to be verified as artists.

Admin Dashboard: A private interface for the System Admin to approve or reject artist requests.

Hardware Interfaces
The software must interact with specific physical and logical hardware components to manage high-fidelity audio and generative AI tasks.

Supported Device Types: The application shall support mobile handsets running Android 11.0+ or iOS 15.0+ with a minimum of 4 GB RAM to ensure stable HLS decoding.
Audio Output Interface: The system interfaces with the device's Digital-to-Analog Converter (DAC) via the operating system's audio layer to provide sound to internal speakers, wired headphones, or Bluetooth peripherals.
Network Interface Card (NIC): The software utilizes the device's Wi-Fi or Cellular radio hardware for asynchronous communication with the .NET backend and external AI APIs.
Control Interactions: Hardware buttons (Volume Up/Down, Play/Pause, Power) must be mapped through the mobile OS to control the application's media state.
Software Interfaces
EchoFlow AI relies on a distributed software stack to manage streaming, social features, and generative tasks.

Operating Systems: The client-side operates on Android (API 30+) and iOS (15.0+), while the server-side operates on Ubuntu 22.04 LTS or Windows Server 2025.
Database Management: Stores relational data including user profiles, metadata, and the parent_idlinks for nested comments.
Incoming Data: User IDs, encrypted passwords, comment strings, track IDs.
Outgoing Data: Profile information, threaded comment lists, verified artist status.
External AI API (e.g., Suno/Meta): Handles the music generation requests.
Outgoing Message: Structured JSON prompt (Genre, Mood) and Reference Track ID.
Incoming Message: URL or binary stream of the generated remix.
Entity Framework Core (.NET): Facilitates the data sharing mechanism between the API and the PostgreSQL database.
3.4 Communications Interfaces
This section defines the protocols and security standards for data exchange across the network.

HTTP/S (REST): The primary protocol for standard operations such as user registration, artist verification applications, and metadata retrieval.
Security & Encryption**:**
JWT (JSON Web Tokens): Used for session synchronization and role-based access control.
System Features / Requirements
4.1User Class: Listener (General User)
This class represents the primary user of the application who consumes music and interacts with AI features.

4.1.1 Feature : AI Remix Generation

Description and Priority: High Priority. Allows users to select a reference track and apply style transfers via an external AI API to create a new version.

Stimulus/Response Sequences:

Stimulus: User clicks "Remix This" on an original track.
Response: System displays the Prompt Builder with modular options (Genre, Mood).
Stimulus: User selects options and clicks "Generate."
Response: System sends a request to the .NET backend, which forwards it to the Cloud AI API.
Response: Once the API returns the result, the system notifies the user and adds the track to the "Generated" feed.
4.2 User Class: Artist
This class represent verified users who provide the original content for the platform.

4.2.1 Feature: Original Content Upload
Description and Priority: High Priority. Enables verified artists to upload high-fidelity audio and manage metadata.

Stimulus/Response Sequences:

Stimulus: Artist navigates to the "Upload" screen and selects a file.
Response: System validates file format (WAV/MP3) and size.
Stimulus: Artist enters metadata and toggles "Allow AI Remixing."
Response: System uploads the file to S3 and segments it for HLS streaming.
4.3 User Class: Administrator
The System Admin manages the integrity of the platform and verifies professional accounts.

4.3.1 Feature: Artist Verification
Description and Priority: High Priority. A workflow for reviewing and approving user applications for artist status.

Stimulus/Response Sequences:

Stimulus: Admin logs into the dashboard and sees pending applications.
Response: System displays user-provided credentials and identity proofs.
Stimulus: Admin clicks "Verify Artist."
Response: System updates the user's role in the PostgreSQL database and enables upload features for that user.
Functional Requirements
4.4.1 User Class: Listener(General User)
D: FR1

TITLE: Download mobile application

DESC: A user should be able to download the mobile application through either an application store or similar service on the mobile phone. The application should be free to download.

RAT: In order for a user to download the mobile application.

DEP: None

ID: FR2

TITLE: User registration - Mobile application

DESC: Given that a user has downloaded the mobile application, then the user should be able to register through the mobile application. The user must provide user-name, password and e-mail address. The user can choose to provide a regularly used phone number.

RAT: In order for a user to register on the mobile application.

DEP: FR1

ID: FR3

TITLE: User log-in - Mobile application

DESC: Given that a user has registered, then the user should be able to log in to the mobile application. The log-in information will be stored on the phone and in the future the user should be logged in automatically.

RAT: In order for a user to register on the mobile application.

DEP: FR1, FR3

ID: FR4

TITLE: Retrieve password

DESC: Given that a user has registered, then the user should be able to retrieve his/her password by e-mail.

RAT: In order for a user to retrieve his/her password. DEP: FR1

ID: FR5

TITLE: Mobile application - Search

DESC: Given that a user is logged in to the mobile application, then the first page that is shown should be the search page. The user should be able to search for a xxxx, according to several search options. The search options are xxx, xxxx, xxxxx and xxxx. There should also be a free-text search option. A user should be able to select multiple search options in one search.

RAT: In order for a user to search for a xxxxx.

ID: FR6

TITLE: Mobile application - Profile page

DESC: On the mobile application, a user should have a profile page. On the profile page a user can edit his/her information, which includes the password, e-mail address and phone number. A user should also be able to choose what language the mobile application should be set to. The different language choices are Turkish and English.

RAT: In order for a user to have a profile page on the mobile application.

DEP: FR1

ID: FR7 Feature: Create an account

ID: FR8 Feature: Delete an account

ID: FR9 Feature: Update an account

ID: FR10 Feature: Manage information

ID: FR11

TITLE: AI Remix Selection

DESC: Given that a user is on a track page, they shall be able to trigger the "Remix" function. The system shall display a modular Prompt Builder with categories: Genre, Mood, and Instruments.

RAT: To allow users to configure AI generation parameters.

DEP: FR3

ID: FR12

TITLE: API Generation Request

DESC: The system shall send the user's prompt and the original track ID to the .NET backend, which will then request a remix from the external Cloud AI API.

RAT: To perform the core "Suno-style" generation via external services

DEP: FR11

ID: FR13

TITLE: Generation Notification

DESC: Once the external API returns the generated audio, the system shall notify the user via a push notification and list the track in the user's "Generated" feed.

RAT: To handle the asynchronous nature of AI processing.

DEP: FR2

ID: FR14

TITLE: Nested Commenting

DESC: A user shall be able to comment on any track and reply to existing comments. The system must maintain a hierarchical thread structure (replies to replies).

RAT: To facilitate community discussion and social engagement.

DEP: FR2

ID: FR15

TITLE: Like/Save Content

DESC: A user shall be able to "Like" a track or add it to a personal playlist. The system shall update the track's like count in real-time.

RAT: To allow users to curate their personal library.

DEP: FR3

4.4.2 User Class: Artist
ID: FR16

TITLE: Artist Application

DESC: A registered user shall be able to apply for "Artist" status by submitting a portfolio or identity proof through a dedicated form.

RAT: To initiate the verification process for content creators.

DEP: FR2

ID: FR17

TITLE: Track Upload

DESC: Verified artists shall be able to upload audio files (WAV/MP3) along with metadata (Title, Genre, Cover Art).

RAT:To populate the platform with original music.

DEP: FR20 (Admin Approval)

ID: FR18

TITLE: AI Permission Toggle

DESC: During upload or via the track settings, the artist shall be able to enable or disable the "Allow AI Remixing" flag for their specific track.

RAT: To give artists control over the derivative use of their intellectual property.

DEP: FR17

4.4.3 User Class: Administrator
ID: FR19

TITLE: Admin Authentication

DESC: The system shall provide a secure login for administrators to access the management dashboard.

RAT: To protect sensitive administrative functions.

DEP: FR1

ID: FR20

TITLE: Verify Artist Status

DESC: The Admin shall be able to review pending artist applications and approve or reject them. Approval must trigger a role change for the user in the database.

RAT: To ensure only legitimate creators can upload original content.

DEP: FR16

ID: FR21

TITLE: Content Moderation

DESC: The Admin shall be able to delete any track or comment that violates community guidelines or copyright policies.

RAT: To maintain a safe and legal platform environment.

DEP: FR19

4.5 Error Handling & Invalid Inputs
Network Interruption during Upload: If the connection is lost during a track upload (FR19), the system shall pause the task and notify the artist to resume once the connection is restored.

Invalid API Response: If the external AI API returns an error or an incompatible format (FR13), the system shall inform the user that "Remixing is currently unavailable" and shall not consume any user generation credits (TBD).

Duplicate Registration: If a user tries to register with an email already in the system (FR3), the system shall display an error message: "Email already in use"

Nonfunctional System Requirements
5.1. Performance Requirements

PERF-1: The REST API must respond to standard queries (e.g., fetching a profile) within 300ms.

PERF-2: Audio streaming must begin buffering within 1 second of the user initiating playback on a standard 4G/WiFi connection.

5.2. Safety Requirements

SAFE-1: The system shall utilize soft deletes (via AuditableBaseEntity) to ensure that accidentally deleted entities (users, tracks, comments) can be recovered by a database administrator.

5.3. Security Requirements

SEC-1: All communication between the mobile/web client and the backend must be encrypted using HTTPS/TLS.

SEC-2: User authentication must be handled via secure JSON Web Tokens (JWT), with tokens securely stored on the client side.

SEC-3: Passwords must be securely hashed before being stored in the database.

5.4. Software Quality Attributes

Maintainability: The backend codebase strictly adheres to Clean Architecture, decoupling business logic from infrastructure to ensure long-term maintainability. The frontend follows a feature-based folder structure.

Scalability: The application architecture supports horizontal scaling, facilitated by Docker containerization.

5.5. Business Rules

BR-1: A standard user cannot upload or publicly release tracks until their ArtistApplication has been approved by an administrator.
BR-2: Users must be authenticated to interact with social features (likes, comments, following).
Other Requirements
Deployment: The backend ecosystem is orchestrated using docker-compose.yml, requiring a Docker environment for standard deployment.
Content Delivery: High-bandwidth media files (audio tracks) require integration with a CDN (e.g., AWS CloudFront) for efficient global deliver
System Models
System Context
Resim 3

Figure 7.1 System Context Diagram

The System Context diagram illustrates the high-level relationship between Mozart-AI and its external environment. It identifies the primary actors-Standard Users (Listeners), Artists, and Administrators-and the external interfaces such as the Generative AI API and Cloud Storage services. This provides a foundational overview of the system's boundary and communication flows.

Interaction Models
7.2.1Use-Case Models
Use Case ID	Name	Actor	Description
UC-1	Generate AI Track	Artist/User	User provides prompt tags to generate a new audio track.
UC-2	Apply as Artist	Listener	User submits ID and portfolio links to become an artist.
UC-3	View Analytics	Artist	Artist views the web dashboard to see track statistics.
UC-4	Stream Music	Listener	User discovers and plays a track from the library.
7.3 System Models
Comments: Requires a stable internet connection. The system supports background playback on mobile devices.

Response: Media player launches, audio begins buffering within 1 second, and track metadata is displayed.

Stimulus: User taps the play button on a track card.

Data: Track metadata (title, artist, cover image), audio stream URL (CDN), playback position.

Description: A user browses the Home/Discover feed, selects a track, and initiates playback. The system fetches the audio stream URL from cloud storage and begins buffering.

Actors: Listener

UC-4: Stream Music

Comments: Data is aggregated server-side; charts reflect near-real-time statistics.

Response: System renders statistical charts and summary cards with up-to-date track performance data.

Stimulus: Artist opens the Analytics section of the web dashboard.

Data: TrackStatistic records, play counts, engagement metrics, time-series chart data.

Description: An approved artist navigates to the web dashboard to view performance statistics for their tracks, including play counts, like counts, comment counts, and growth trend charts.

Actors: Artist

UC-3: View Analytics

Comments: The application status transitions through Pending, Approved, and Rejected states as modeled in Figure 7.7.

Response: System acknowledges submission and notifies the user of the application outcome via the platform.

Stimulus: User submits the artist application form from their profile settings.

Data: User ID, government-issued ID document, portfolio URL, application status.

Description: A standard user who wishes to become an artist submits an ArtistApplication containing a government-issued ID and portfolio links. An administrator reviews the application and either approves or rejects it.

Actors: Listener (Standard User)

UC-2: Apply as Artist

Comments: The user must have an active internet connection. Rate limiting may apply to prevent API abuse.

Response: System confirms successful track generation and displays the new track in the user's library.

Stimulus: User submits the AI generation form via the app interface.

Data: Mood tags, instrument tags, raw text prompt, generated audio file URL.

Description: An authenticated user inputs mood tags, instrument tags, and a raw text prompt. The system creates a GenerativePrompt record and forwards the request to the external AI music generation API. Upon success, a new Track entity is returned and saved to the user's profile.

Actors: Artist / Standard User

UC-1: Generate AI Track

Resim 4

Figure 7.2: Use-Case Diagram

The Use-Case diagram maps the primary interactions between the actors and the system's core features. It identifies four high-level use cases: Generate AI Track (UC-1), Apply as Artist (UC-2), View Analytics (UC-3), and Stream Music (UC-4), detailing how different user roles achieve their goals within the platform.

7.3.1. Sequence Diagrams
Resim 5

Figure 7.3: Artist Application Sequence Diagram

This sequence diagram outlines the multi-step verification process for artist certification: 1. The user submits portfolio and ID proof via the client. 2. The system creates a pending ArtistApplication. 3. An administrator reviews applications through the dashboard. 4. Upon approval, the user's role is updated and a notification is dispatched.

7.4 Structural Models
7.4.1. Object and Class Model
Resim 6

Figure 7.4: Domain Class Diagram

The Domain Class diagram defines the static data model of Mozart-AI. It illustrates relationships between Users, Artists, Tracks, and Playlists. Key entities like Genre, Like, and Comment are shown with their multiplicities, forming the foundation for the relational database schema and Entity Framework Core mappings.

Important Domain Classes:
User: Represents every registered entity on the platform. Stores basic credentials, preferences, and social links. Standard users can follow artists and manage playlists.
Artist: An extension of the User entity for content creators. Linked to tracks and analytics, allowing for content management through the specialized dashboard.
Track: The primary content entity. Contains references to audio files on the CDN, genre classification, and mood tags used for discovery and AI generation.
Playlist: A collection of tracks curated by users. Utilizes the PlaylistTrack entity to support many-to-many relationships with track order preservation.
ArtistApplication: Manages the state and data of a user's request to become an artist, including portfolio links and administrator review outcomes.

7.5. Behavioral Models
7.5.1. Data Driven (Activity Diagrams)
Resim 7

Figure 7.5: Track Generation Activity Diagram

The Activity diagram captures the dynamic workflow of AI music generation. It models the logic from initial prompt input (mood and instrument tags) through asynchronous external AI service interaction, ending with the creation of a new Track entity and its persistence in the user's library.

7.5.2. Data Driven (Sequence Diagrams)
Resim 8

Figure 7.6: Music Playback Sequence Diagram

This diagram illustrates the real-time interaction during track playback. The client requests a stream URL from the backend, which validates the session and returns a secure CDN link. While the audio engine buffers the track, the system asynchronously updates the TrackStatistic entity to reflect the play event for artist analytics.

7.5.3. Event Driven Models (State Diagrams)
Resim 17Figure 7.7: ArtistApplication State Diagram

The State diagram describes the lifecycle of an ArtistApplication. Records transition from 'Pending' upon submission to 'Under Review' during admin processing, finally reaching terminal 'Approved' or 'Rejected' states. This ensures a controlled workflow for user role upgrades.

User Interface
The Mozart-AI application provides distinct UI experiences for mobile and web platforms, both built using Flutter for cross-platform consistency.

Simulator Screenshot - iPhone 16 - 2026-04-26 at 17.41.57.pngSimulator Screenshot - iPhone 16 - 2026-04-26 at 17.41.52.png

Figure 7.8 Auth Screen (Login / Registration)

Screen 1 - Auth Screen (Login / Registration): The entry point for new and returning users. Features email/password fields, a "Forgot Password" link, and a registration toggle. Clean, minimal design with the Mozart-AI branding.

image10.png

Figure 7.9: Home and Discover

Home / Discover Tab: The primary landing screen after login. Displays a horizontally scrolling "New Releases" section and a vertically scrolling "Trending Tracks" feed. Each track card shows the cover image, title, artist name, and play count.

Simulator Screenshot - iPhone 16 - 2026-04-26 at 16.40.57.png

Figure 7.10 :Mobile Music Player Interface

Music Player: A full-screen player overlay displaying the track cover art, title, artist name, progress bar, playback controls (previous, play/pause, next), and a like button. Supports background playback on mobile.

 

Figure 7.11 Profile Screen

Profile Screen: Displays the user's avatar, name, follower/following counts, and their playlist library.

Screenshot 2026-04-26 at 15.49.27.png Figure7.11 Artist Web Dashboard

Screen 5 - Artist Web Dashboard: A sidebar-based web layout accessible only to Artists and Admins. Features a navigation sidebar, a stats overview panel (plays, likes, comments), and interactive growth

charts (line chart over time). The moderation panel for admins is accessible from this dashboard.

References
IEEE Recommended Practice for Software Requirements Specifications (Standard 830-1998).
Object-Oriented Software Engineering, Using UML, Patterns, and Java, 2nd Edition, by Bernd Bruegge and Allen H. Dutoit, Prentice-Hall, 2004, ISBN: 0-13-047110-0.
.NET Documentation: Official Microsoft technical documentation for backend development.
Flutter Documentation: Official Google technical documentation for cross-platform mobile development
Appendix A: Glossary
Software Requirements Specification: A document that describes what the software will do and how it will be expected to perform.
Application Programming Interface: A set of rules that allow different software entities to communicate with each other
HTTP Live Streaming: An HTTP-based adaptive bitrate streaming communications protocol
Nested Comments: A hierarchical comment structure where users can reply to specific comments, creating a "thread"
Inference: The process of using a trained AI model to generate new data (e.g., generating music from a prompt).
JSON Web Token: An open standard used to share security information between a client and a server
Role-Based Access Control: A method of regulating access to computer or network resources based on the roles of individual users (e.g., Admin, Artist, Listener).
To Be Determined: A placeholder indicating that more information is required before a requirement can be finalized
Riverpod: A reactive state-management framework for Flutter applications.
Team Members, Roles and CVs