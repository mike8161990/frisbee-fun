export interface CatchEvent {
    timestamp: Date;
    telemetryPoints: TelemetryPoint[];
}

export interface TelemetryPoint {
    timestamp: Date;
    accelerationX: number;
    accelerationY: number;
    accelerationZ: number;
}
