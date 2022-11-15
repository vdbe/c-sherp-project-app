FROM mcr.microsoft.com/devcontainers/dotnet:0-6.0

# Install maui
RUN dotnet workload install maui-android

# Install adb & JDK.
RUN apt-get update \
	&& apt-get install --yes --no-install-recommends \
		android-tools-adb default-jdk-headless \
	&& apt-get clean --yes \
	&& rm -rf /var/lib/apt/lists/*

# Install Android SDK.
ENV AndroidSdkDirectory=/usr/lib/android-sdk
RUN TMP_DIR=$(mktemp -d -t android-sdk-XXXX) \
	&& echo "${TMP_DIR}" \
	&& cd "${TMP_DIR}" \
	&& curl -sSL https://dl.google.com/android/repository/commandlinetools-linux-8512546_latest.zip -o cmdline-tools.zip \
	&& unzip cmdline-tools.zip \
	&& cd cmdline-tools/bin \
	&& yes | ./sdkmanager --sdk_root=${AndroidSdkDirectory} --install \
		"cmdline-tools;8.0" \
		"build-tools;32.0.0" \
		"platform-tools" \
		"platforms;android-31" \
	&& rm -rf "${TMP_DIR}"
