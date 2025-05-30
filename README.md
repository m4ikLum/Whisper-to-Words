# Whisper-to-Words

Children with autism face barriers to effective speech therapy due to high costs, limited availability, and lack of accessible transportation. This project introduces an innovative, AI-driven solution that offers an accessible, real-time speech assessment application built in Unity and powered by OpenAI's Whisper model via Undertone. Users are prompted with a set of 370 target words. Their spoken responses are transcribed and scored using a custom-built 370 × 13,000 similarity matrix. Unlike existing tools that rely on amplitude of speech, this approach evaluates pronunciation accuracy based on phonetic similarity, providing immediate numerical feedback on spoken words, and reinforcing correct speech patterns. Early testing with native and non-native speakers demonstrated significant improvement after iterative matrix refinements to account for issues including homophones, pluralization, and dialectal differences. Refinement improved the app’s scoring accuracy from 65% to 95% for perfect pronunciations, 13% to 80% for pronunciations characteristic of early speech development, and 58% to 73% for babbling articulation, exhibiting a consistent upward trend across tests. This project represents a significant step toward scalable, cost-effective therapy solutions that empower families and educators, while advancing inclusive technologies for neurodiverse populations. It lays the groundwork for broader adoption of AI in therapeutic settings, bridging the gap between clinical care and everyday learning environments.

# To Use:
Download the game engine Unity and upload the folders contained on this page: Assets, Library, Logs, Packages, ProjectSettings, UserSettings. 

Note: unzip the following before use
   1) Assets/Undertone/Plugins/Windows/x64/onnxruntime_providers_cuda.dll.zip
   2) Assets/Undertone/Plugins/iOS/libonnxruntime.a.zip

The report details documentation of the project as well as our findings after rounds of testing. 
