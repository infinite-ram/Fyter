function scrollToFirstError() {
  const firstError = document.querySelector(".validation-message:not(:empty)");
  if (firstError) {
    firstError.scrollIntoView({ behavior: "smooth", block: "center" });
  }
}
