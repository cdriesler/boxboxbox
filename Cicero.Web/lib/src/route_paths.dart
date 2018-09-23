import "package:angular_router/angular_router.dart";

const idParam = "id";

class RoutePaths {
  static final project = RoutePath(path: "${projects.path}/:$idParam");
  static final projects = RoutePath(path: "projects");
  static final dashboard = RoutePath(path: "dashboard");
}

String getId(Map<String, String> parameters) {
  final id = parameters[idParam];
  return id == null ? null : id;
}